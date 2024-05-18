using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlPrograms : DapperSql<Programs>
    {
        public z_sqlPrograms()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Programs.SortNo ASC, Programs.PrgNo ASC";
            DefaultOrderByDirection = "";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Programs.Id, Programs.IsEnabled, Programs.IsPageSize, Programs.IsSearch, 
Programs.RoleNo, Roles.RoleName, Programs.ModuleNo, Modules.ModuleName, 
Programs.SortNo, Programs.PrgNo, Programs.PrgName, Programs.CodeNo, 
vi_CodeProgram.CodeName, Programs.AreaName, Programs.ControllerName, 
Programs.ActionName, Programs.ParmValue, Programs.PageSize , Remark 
FROM Programs 
INNER JOIN Modules ON Programs.ModuleNo = Modules.ModuleNo 
INNER JOIN Roles ON Modules.RoleNo = Roles.RoleNo 
INNER JOIN vi_CodeProgram ON Programs.CodeNo = vi_CodeProgram.CodeNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Programs.RoleNo",
                    "Roles.RoleName",
                    "Programs.ModuleNo",
                    "Modules.ModuleName",
                    "Programs.PrgNo",
                    "Programs.PrgName",
                    "vi_CodeProgram.CodeName",
                    "Programs.AreaName",
                    "Programs.ControllerName",
                    "Programs.ActionName",
                    "Programs.Remark"
                     };
            return searchColumn;
        }

        /// <summary>
        /// 取得下拉式選單資料集
        /// </summary>
        /// <param name="showNo">是否顯示編號</param>
        /// <returns></returns>
        public List<SelectListItem> GetDropDownList(bool showNo = true)
        {
            using var dpr = new DapperRepository();
            string str_query = "SELECT ";
            if (showNo) str_query += "PrgNo + ' ' + ";
            str_query += "PrgName AS Text , PrgNo AS Value FROM Programs ORDER BY PrgNo";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }

        public List<SelectListItem> GetDropDownList(string moduleNo, bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"PrgNo + ' ' + ";
            str_query += "PrgName AS Text , PrgNo AS Value FROM Programs ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY PrgNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("ModuleNo", moduleNo);
            var model = dpr.ReadAll<SelectListItem>(str_query, parm);
            return model;
        }

        public override string GetSQLWhere()
        {
            string str_query = " WHERE Programs.ModuleNo = @ModuleNo ";
            return str_query;
        }

        /// <summary>
        /// 取得單筆資料(同步呼叫)
        /// </summary>
        /// <param name="areaName">Area 名稱</param>
        /// <param name="controllerName">Controller 名稱</param>
        /// <returns></returns>
        public Programs GetData(string areaName, string controllerName)
        {
            var model = new Programs();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE Programs.AreaName = @AreaName AND Programs.ControllerName = @ControllerName ";
            sql_query += sql_where;
            DynamicParameters parm = new DynamicParameters();
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                parm.Add("AreaName", areaName);
                parm.Add("ControllerName", controllerName);
            }
            model = dpr.ReadSingle<Programs>(sql_query, parm);
            return model;
        }
        /// <summary>
        /// 取得單筆資料(同步呼叫)
        /// </summary>
        /// <param name="prgNo">程式代號</param>
        /// <returns></returns>
        public Programs GetData(string prgNo)
        {
            var model = new Programs();
            if (string.IsNullOrEmpty(prgNo))
            {
                //新增預設值
            }
            else
            {
                using var dpr = new DapperRepository();
                string sql_query = GetSQLSelect();
                string sql_where = " WHERE (Programs.PrgNo = @PrgNo) ";
                sql_query += sql_where;
                sql_query += GetSQLOrderBy();
                DynamicParameters parm = new DynamicParameters();
                if (!string.IsNullOrEmpty(sql_where))
                {
                    //自定義的 Weher Parm 參數
                    parm.Add("PrgNo", prgNo);
                }
                model = dpr.ReadSingle<Programs>(sql_query, parm);
            }
            return model;
        }
        /// <summary>
        /// 取得多筆資料(同步呼叫)
        /// </summary>
        /// <param name="moduleNo">模組編號</param>
        /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
        /// <returns></returns>
        public List<Programs> GetDataList(string moduleNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Programs>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = GetSQLWhere();
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(new Programs(), searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Weher Parm 參數
                parm.Add("ModuleNo", moduleNo);
            }
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Programs>(sql_query, parm);
            return model;
        }
        /// <summary>
        /// 取得多筆資料(同步呼叫)
        /// </summary>
        /// <param name="moduleNo">模組編號</param>
        /// <returns></returns>
        public List<Programs> GetMenuDataList(string moduleNo)
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Programs>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE Programs.ModuleNo = @ModuleNo AND Programs.IsEnabled = @IsEnabled ";
            sql_query += sql_where;
            parm.Add("ModuleNo", moduleNo);
            parm.Add("IsEnabled", true);
            sql_query += GetSQLOrderBy();
            model = dpr.ReadAll<Programs>(sql_query, parm);
            return model;
        }

        public void SetCurrentPrgInfo()
        {
            var data = GetData(ActionService.Area, ActionService.Controller);
            //SessionService.SetProgramInfo(data);
        }
    }
}