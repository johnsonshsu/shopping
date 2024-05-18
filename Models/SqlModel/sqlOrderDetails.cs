using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlOrderDetails : DapperSql<OrderDetails>
    {
        public z_sqlOrderDetails()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "OrderDetails.ProdNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT OrderDetails.Id, OrderDetails.ParentNo, OrderDetails.VendorNo, 
OrderDetails.CategoryNo, Categorys.CategoryName,OrderDetails.ProdNo, 
OrderDetails.ProdName, OrderDetails.ProdSpec, OrderDetails.OrderPrice, 
OrderDetails.OrderQty, OrderDetails.OrderAmount, OrderDetails.Remark
FROM OrderDetails 
LEFT OUTER JOIN Categorys ON OrderDetails.CategoryNo = Categorys.CategoryNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Roles.RoleName",
                    "OrderDetails.ModuleNo",
                    "OrderDetails.ModuleName",
                    "OrderDetails.ModuleNo"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"ProdNo + ' ' + ";
            str_query += "ProdName AS Text , ProdNo AS Value FROM OrderDetails ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY ProdNo";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }
        /// <summary>
        /// 購物車資訊新增到訂單明細
        /// </summary>
        /// <param name="orderNo">訂單編號</param>
        /// <returns></returns>
        public void CreateNewOrderDetail(string orderNo)
        {
            using var dpr = new DapperRepository();
            using var cart = new z_sqlCarts();
            var CartList = cart.GetDataList();
            string str_query = @"
INSERT INTO OrderDetails
(ParentNo,VendorNo,CategoryNo,ProdNo,ProdName,ProdSpec
,OrderPrice,OrderQty,OrderAmount,Remark)
VALUES
(@ParentNo,@VendorNo,@CategoryNo,@ProdNo,@ProdName,@ProdSpec
,@OrderPrice,@OrderQty,@OrderAmount,@Remark)
";
            foreach (var item in CartList)
            {
                DynamicParameters parm = new DynamicParameters();
                parm.Add("ParentNo", orderNo);
                parm.Add("VendorNo", "");
                parm.Add("CategoryNo", item.CategoryNo);
                parm.Add("ProdNo", item.ProdNo);
                parm.Add("ProdName", item.ProdName);
                parm.Add("ProdSpec", item.ProdSpec);
                parm.Add("OrderPrice", item.OrderPrice);
                parm.Add("OrderQty", item.OrderQty);
                parm.Add("OrderAmount", item.OrderAmount);
                parm.Add("Remark", "");
                dpr.Execute(str_query, parm);
            }
        }
        /// <summary>
        /// 取得訂單明細
        /// </summary>
        /// <param name="orderNo">訂單編號</param>
        /// <returns></returns>
        public List<OrderDetails> GeOrderDetailList(string orderNo)
        {
            DynamicParameters parm = new DynamicParameters();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            sql_query += " WHERE OrderDetails.ParentNo = @ParentNo ";
            sql_query += " ORDER BY OrderDetails.ProdNo";
            parm.Add("ParentNo", orderNo);
            var model = dpr.ReadAll<OrderDetails>(sql_query, parm);
            return model;
        }
    }
}

