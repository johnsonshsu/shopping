using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlNotifications : DapperSql<Notifications>
    {
        public z_sqlNotifications()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Notifications.SendDate DESC, Notifications.SendTime DESC";
            DefaultOrderByDirection = "";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Notifications.Id, Notifications.IsRead, Notifications.CodeNo,
vi_CodeNotification.CodeName, Notifications.SourceNo, Notifications.SenderNo, 
Users.UserName AS SenderName, Notifications.ReceiverNo, Users_1.UserName AS ReceiverName, 
Notifications.SendDate, Notifications.SendTime, Notifications.HeaderText, 
Notifications.MessageText, Notifications.Remark
FROM Notifications 
LEFT OUTER JOIN Users ON Notifications.SenderNo = Users.UserNo 
LEFT OUTER JOIN Users AS Users_1 ON Notifications.ReceiverNo = Users_1.UserNo 
LEFT OUTER JOIN vi_CodeNotification ON Notifications.CodeNo = vi_CodeNotification.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Roles.RoleName",
                    "Notifications.ModuleNo",
                    "Notifications.ModuleName",
                    "Notifications.ModuleNo"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += "CAST(Id AS varchar)+ ' ' + ";
            str_query += "HeaderText AS Text , ModuleNo AS Id FROM Notifications ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY Id";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }

        /// <summary>
        /// 取得寄件者多筆資料(同步呼叫)
        /// </summary>
        /// <param name="senderNo">寄件者編號</param>
        /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
        /// <returns></returns>
        public List<Notifications> GetSenderDataList(string senderNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Notifications>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE (Notifications.SenderNo = @SenderNo) ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(new Notifications(), searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                parm.Add("SenderNo", senderNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Notifications>(sql_query, parm);
            return model;
        }
        /// <summary>
        /// 取得收件者多筆資料(同步呼叫)
        /// </summary>
        /// <param name="receiverNo">收件者編號</param>
        /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
        /// <returns></returns>
        public List<Notifications> GetReceiverDataList(string receiverNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Notifications>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE (Notifications.ReceiverNo = @ReceiverNo) ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(new Notifications(), searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                parm.Add("ReceiverNo", receiverNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Notifications>(sql_query, parm);
            return model;
        }
        /// <summary>
        /// 取得收件者讀取狀多筆資料(同步呼叫)
        /// </summary>
        /// <param name="receiverNo">收件者編號</param>
        /// <param name="isRead">是否已讀取</param>
        /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
        /// <returns></returns>
        public List<Notifications> GetReceiverUnReadDataList(string receiverNo, bool isRead, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Notifications>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE (Notifications.ReceiverNo = @ReceiverNo AND Notifications.IsRead = @IsRead) ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(new Notifications(), searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                parm.Add("ReceiverNo", receiverNo);
                parm.Add("IsRead", isRead);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Notifications>(sql_query, parm);
            return model;
        }
    }
}