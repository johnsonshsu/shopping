using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlDepartments : DapperSql<Departments>
    {
        public z_sqlDepartments()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Departments.DeptNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Id, DeptNo, DeptName, Remark FROM Departments
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Departments.DeptNo",
                    "Departments.DeptName",
                    "Departments.Remark"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"DeptNo + ' ' + ";
            str_query += "DeptName AS Text , DeptNo AS Value FROM Departments ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY DeptNo";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }
    }
}