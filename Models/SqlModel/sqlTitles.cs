using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlTitles : DapperSql<Titles>
    {
        public z_sqlTitles()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Titles.TitleNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Id, TitleNo, TitleName, Remark FROM Titles
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Titles.TitleNo",
                    "Titles.TitleName",
                    "Titles.Remark"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"TitleNo + ' ' + ";
            str_query += "TitleName AS Text , TitleNo AS Value FROM Titles ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY TitleNo";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }
    }
}