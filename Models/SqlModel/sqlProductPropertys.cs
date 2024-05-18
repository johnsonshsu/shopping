using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlProductPropertys : DapperSql<ProductPropertys>
    {
        public z_sqlProductPropertys()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "ProductPropertys.PropertyNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT ProductPropertys.Id, ProductPropertys.IsSelect, ProductPropertys.ProdNo, 
ProductPropertys.PropertyNo, Propertys.PropertyName, ProductPropertys.PropertyValue, 
ProductPropertys.Remark 
FROM  ProductPropertys 
INNER JOIN Propertys ON ProductPropertys.PropertyNo = Propertys.PropertyNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "ProductPropertys.TitleNo",
                    "ProductPropertys.TitleName",
                    "ProductPropertys.Remark"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(string prodNo, bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"PropertyNo + ' ' + ";
            str_query += "PropertyValue AS Text , PropertyNo AS Value FROM ProductPropertys ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY PropertyNo";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("ProdNo", prodNo);
            var model = dpr.ReadAll<SelectListItem>(str_query, parm);
            return model;
        }

        public string GetProductSpec(string prodNo)
        {
            string str_value = "";
            using var dpr = new DapperRepository();
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("ProdNo", prodNo);
            var data = dpr.ReadAll<ProductPropertys>(str_query, parm);
            foreach (var item in data)
            {
                str_value += $"{item.PropertyName}:{item.PropertyValue} ";
            }
            return str_value.Trim();
        }

        public List<ProductPropertys> GetProductSpecList(string prodNo)
        {
            using var dpr = new DapperRepository();
            string str_query = GetSQLSelect();
            str_query += GetSQLWhere();
            DynamicParameters parm = new DynamicParameters();
            parm.Add("ProdNo", prodNo);
            var data = dpr.ReadAll<ProductPropertys>(str_query, parm);
            return data;
        }

        public override string GetSQLWhere()
        {
            string str_query = " WHERE ProdNo = @ProdNo ";
            return str_query;
        }
    }
}