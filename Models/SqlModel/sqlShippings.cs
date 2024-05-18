using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlShippings : DapperSql<Shippings>
    {
        public z_sqlShippings()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Shippings.ShippingNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Id, ShippingNo, ShippingName, Remark FROM Shippings
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Shippings.ShippingNo",
                    "Shippings.ShippingName",
                    "Shippings.Remark"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"ShippingNo + ' ' + ";
            str_query += "ShippingName AS Text , ShippingNo AS Value FROM Shippings ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY ShippingNo";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }
    }
}