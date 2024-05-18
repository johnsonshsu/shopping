using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlOrders : DapperSql<Orders>
    {
        public z_sqlOrders()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Orders.SheetNo";
            DefaultOrderByDirection = "DESC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Orders.Id, Orders.SheetNo, Orders.SheetDate, Orders.StatusCode, 
OrdersStatus.StatusName, Orders.IsClosed, Orders.IsValid, Orders.CustNo, 
Orders.CustName, Orders.PaymentNo, Payments.PaymentName, Orders.ShippingNo, 
Shippings.ShippingName, Orders.ReceiverName, Orders.ReceiverEmail, 
Orders.ReceiverAddress, Orders.OrderAmount, Orders.TaxAmount, 
Orders.TotalAmount, Orders.Remark, Orders.GuidNo
FROM Orders 
LEFT OUTER JOIN Shippings ON Orders.ShippingNo = Shippings.ShippingNo 
LEFT OUTER JOIN Payments ON Orders.PaymentNo = Payments.PaymentNo 
LEFT OUTER JOIN OrdersStatus ON Orders.StatusCode = OrdersStatus.StatusNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Orders.SheetNo",
                    "OrdersStatus.StatusName",
                    "Payments.PaymentName",
                    "Orders.CustNo",
                    "Orders.CustName",
                    "Shippings.ShippingName",
                    "Orders.ReceiverName",
                    "Orders.ReceiverEmail",
                    "Orders.ReceiverAddress",
                    "Orders.Remark"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"SheetNo + ' ' + ";
            str_query += "CustName AS Text , SheetNo AS Value FROM Orders ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY SheetNo";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }

        /// <summary>
        /// 取得單筆資料(同步呼叫)
        /// </summary>
        /// <param name="sheetNo">訂單編號</param>
        /// <returns></returns>
        public Orders GetData(string sheetNo)
        {
            var model = new Orders();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            sql_query += " WHERE Orders.SheetNo = @SheetNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("SheetNo", sheetNo);
            model = dpr.ReadSingle<Orders>(sql_query, parm);
            return model;
        }

        /// <summary>
        /// 結帳付款產生訂單表頭
        /// </summary>
        /// <param name="model">付款資訊</param>
        /// <returns>訂單編號</returns>
        public string CreateNewOrder(vmOrders model)
        {
            string str_order_no = "";
            int int_amount = 0;
            int int_tax = 0;
            int int_total = 0;
            string str_guid = Guid.NewGuid().ToString().Replace("-", "");
            using var dpr = new DapperRepository();
            //計算購物車中的合計金額
            using var cart = new z_sqlCarts();
            var CartList = cart.GetDataList();
            int_amount = CartList.Sum(m => m.OrderQty * m.OrderPrice);
            double dbl_amount = Convert.ToDouble(int_amount);
            if (dbl_amount != 0) int_tax = Convert.ToInt32(Math.Round((dbl_amount * 5 / 100), 0));
            int_total = int_amount + int_tax;
            //新增訂單表頭
            string str_query = @"
INSERT INTO Orders
(SheetNo,SheetDate,StatusCode,IsClosed,IsValid
,CustNo,CustName,PaymentNo,ShippingNo,ReceiverName
,ReceiverEmail,ReceiverAddress,OrderAmount
,TaxAmount,TotalAmount,Remark,GuidNo) 
VALUES 
(@SheetNo,@SheetDate,@StatusCode,@IsClosed,@IsValid
,@CustNo,@CustName,@PaymentNo,@ShippingNo,@ReceiverName
,@ReceiverEmail,@ReceiverAddress,@OrderAmount
,@TaxAmount,@TotalAmount,@Remark,@GuidNo) 
";
            DynamicParameters parm2 = new DynamicParameters();
            parm2.Add("SheetNo", "");
            parm2.Add("SheetDate", DateTime.Today);
            parm2.Add("StatusCode", "NP");
            parm2.Add("IsClosed", false);
            parm2.Add("IsValid", true);
            parm2.Add("CustNo", SessionService.UserNo);
            parm2.Add("CustName", SessionService.UserName);
            parm2.Add("PaymentNo", model.PaymentNo);
            parm2.Add("ShippingNo", model.ShippingNo);
            if (model.IsDiffenceMember == "on")
            {
                parm2.Add("ReceiverName", model.ReceiveName);
                parm2.Add("ReceiverEmail", model.ReceiveEmail);
                parm2.Add("ReceiverAddress", model.ReceiveAddress);
            }
            else
            {
                parm2.Add("ReceiverName", model.MemberName);
                parm2.Add("ReceiverEmail", model.MemberEmail);
                parm2.Add("ReceiverAddress", model.MemberAddress);
            }
            parm2.Add("OrderAmount", int_amount);
            parm2.Add("TaxAmount", int_tax);
            parm2.Add("TotalAmount", int_total);
            parm2.Add("Remark", model.Remark);
            parm2.Add("GuidNo", str_guid);
            dpr.Execute(str_query, parm2);
            //取得訂單Id , 更新訂單編號
            str_query = GetSQLSelect();
            str_query += " WHERE GuidNo = @GuidNo";
            DynamicParameters parm3 = new DynamicParameters();
            parm3.Add("GuidNo", str_guid);
            var data = dpr.ReadSingle<Orders>(str_query, parm3);
            if (data != null)
            {
                int int_id = data.Id;
                str_order_no = int_id.ToString().PadLeft(8, '0');
                str_query = "UPDATE Orders SET SheetNo = @SheetNo WHERE Id = @Id";
                DynamicParameters parm4 = new DynamicParameters();
                parm4.Add("Id", int_id);
                parm4.Add("SheetNo", str_order_no);
                dpr.Execute(str_query, parm4);
            }
            return str_order_no;
        }
        /// <summary>
        /// 取得會員訂單
        /// </summary>
        /// <returns></returns>
        public List<Orders> GetOrderList()
        {
            var model = new List<Orders>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            sql_query += " WHERE CustNo = @CustNo";
            sql_query += " ORDER BY SheetNo DESC";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("CustNo", SessionService.UserNo);
            model = dpr.ReadAll<Orders>(sql_query, parm);
            return model;
        }
        /// <summary>
        /// 取消訂單
        /// </summary>
        /// <param name="sheetNo">訂單編號</param>
        public void CancelOrder(string sheetNo)
        {
            using var dpr = new DapperRepository();
            string sql_query = "UPDATE Orders SET StatusCode = @StatusCode WHERE SheetNo = @SheetNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("SheetNo", sheetNo);
            parm.Add("StatusCode", "CC");
            dpr.Execute(sql_query, parm);
        }
    }
}