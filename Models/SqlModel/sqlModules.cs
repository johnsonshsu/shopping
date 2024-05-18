using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlModules : DapperSql<Modules>
    {
        public z_sqlModules()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Modules.RoleNo ASC , Modules.SortNo ASC , Modules.ModuleNo ASC";
            DefaultOrderByDirection = "";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Modules.Id, Modules.IsEnabled, Modules.IsWorkflow, Modules.RoleNo, 
Roles.RoleName, Modules.SortNo, Modules.ModuleNo, Modules.ModuleName, 
Modules.IconName, Modules.Remark 
FROM Modules 
LEFT OUTER JOIN Roles ON Modules.RoleNo = Roles.RoleNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Roles.RoleName",
                    "Modules.ModuleNo",
                    "Modules.ModuleName",
                    "Modules.ModuleNo"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"ModuleNo + ' ' + ";
            str_query += "ModuleName AS Text , ModuleNo AS Value FROM Modules ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY ModuleNo";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }

        /// <summary>
        /// 以編號查名稱
        /// </summary>
        /// <param name="dataNo">編號</param>
        /// <returns></returns>
        public string GetDataName(string dataNo)
        {
            using var dpr = new DapperRepository();
            string str_query = "SELECT ModuleName FROM Modules WHERE ModuleNo = @ModuleNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("ModuleNo", dataNo);
            var data = dpr.ReadSingle<Modules>(str_query, parm);
            if (data == null) return "";
            return data.ModuleName;
        }
    }
}