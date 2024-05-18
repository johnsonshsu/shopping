using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlMessages : DapperSql<Messages>
    {
        public z_sqlMessages()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Messages.SendDate DESC, Messages.SendTime DESC";
            DefaultOrderByDirection = "";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Messages.Id, Messages.IsRead, Messages.CodeNo,
vi_CodeMessage.CodeName, Messages.SenderNo, 
Users.UserName AS SenderName, Messages.ReceiverNo, Users_1.UserName AS ReceiverName, 
Messages.SendDate, Messages.SendTime, Messages.HeaderText, 
Messages.MessageText, Messages.Remark
FROM Messages 
LEFT OUTER JOIN Users ON Messages.SenderNo = Users.UserNo 
LEFT OUTER JOIN Users AS Users_1 ON Messages.ReceiverNo = Users_1.UserNo 
LEFT OUTER JOIN vi_CodeMessage ON Messages.CodeNo = vi_CodeMessage.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "vi_CodeMessage.CodeName",
                    "Users.UserName",
                    "Users_1.UserName",
                    "Messages.HeaderText",
                    "Messages.MessageText",
                    "Messages.Remark"
                     };
            return searchColumn;
        }

        /// <summary>
        /// 取得寄件者多筆資料(同步呼叫)
        /// </summary>
        /// <param name="senderNo">寄件者編號</param>
        /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
        /// <returns></returns>
        public List<Messages> GetSenderDataList(string senderNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Messages>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE (Messages.SenderNo = @SenderNo) ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(new Messages(), searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                parm.Add("SenderNo", senderNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Messages>(sql_query, parm);
            return model;
        }

        /// <summary>
        /// 取得收件者多筆資料(同步呼叫)
        /// </summary>
        /// <param name="receiverNo">收件者編號</param>
        /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
        /// <returns></returns>
        public List<Messages> GetReceiverDataList(string receiverNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Messages>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE (Messages.ReceiverNo = @ReceiverNo) ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(new Messages(), searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                parm.Add("ReceiverNo", receiverNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Messages>(sql_query, parm);
            return model;
        }

        /// <summary>
        /// 取得收件者讀取狀多筆資料(同步呼叫)
        /// </summary>
        /// <param name="receiverNo">收件者編號</param>
        /// <param name="isRead">是否已讀取</param>
        /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
        /// <returns></returns>
        public List<Messages> GetReceiverUnReadDataList(string receiverNo, bool isRead, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Messages>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE (Messages.ReceiverNo = @ReceiverNo AND Messages.IsRead = @IsRead) ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(new Messages(), searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                parm.Add("ReceiverNo", receiverNo);
                parm.Add("IsRead", isRead);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Messages>(sql_query, parm);
            return model;
        }
    }
}