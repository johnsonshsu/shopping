using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlRoles : DapperSql<Roles>
    {
        public z_sqlRoles()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Roles.RoleNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Id, RoleNo, RoleName, Remark FROM Roles
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Roles.RoleNo",
                    "Roles.RoleName",
                    "Roles.Remark"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"RoleNo + ' ' + ";
            str_query += "RoleName AS Text , RoleNo AS Value FROM Roles ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY RoleNo";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }
    }
}