using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlCodeDatas : DapperSql<CodeDatas>
    {
        public z_sqlCodeDatas()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "CodeDatas.SortNo ASC,CodeDatas.CodeNo ASC";
            DefaultOrderByDirection = "";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "CodeDatas.CodeNo",
                    "CodeDatas.CodeName",
                    "CodeDatas.Remark"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(string baseNo, bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"CodeNo + ' ' + ";
            str_query += "CodeName AS Text , CodeNo AS Value FROM CodeDatas ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY SortNo , CodeNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("IsEnabled", true);
            parm.Add("BaseNo", baseNo);
            var model = dpr.ReadAll<SelectListItem>(str_query, parm);
            return model;
        }

        public override string GetSQLWhere()
        {
            string str_query = " WHERE IsEnabled = @IsEnabled AND BaseNo = @BaseNo ";
            return str_query;
        }
    }
}