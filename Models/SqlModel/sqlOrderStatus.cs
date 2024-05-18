using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlOrdersStatus : DapperSql<OrdersStatus>
    {
        public z_sqlOrdersStatus()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "StatusNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Id,IsPayed,IsClosed,StatusNo,StatusName,Remark FROM OrdersStatus 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "OrdersStatus.StatusNo",
                    "OrdersStatus.StatusName",
                    "OrdersStatus.Remark"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"StatusNo + ' ' + ";
            str_query += "StatusName AS Text , StatusNo AS Value FROM OrdersStatus ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY StatusNo";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }
    }
}