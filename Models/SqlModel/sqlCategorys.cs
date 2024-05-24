
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlCategorys : DapperSql<Categorys>
    {
        public z_sqlCategorys()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "ParentNo ASC, SortNo ASC, CategoryNo ASC";
            DefaultOrderByDirection = "";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override List<string> GetSearchColumns()
        {
            List<string> searchColumn;
            searchColumn = new List<string>() {
                    "Categorys.CategoryNo",
                    "Categorys.CategoryName",
                    "Categorys.Remark"
                     };
            return searchColumn;
        }

        public List<SelectListItem> GetDropDownList(bool textIncludeValue = false)
        {
            string str_query = "SELECT ";
            if (textIncludeValue) str_query += $"CategoryNo + ' ' + ";
            str_query += "CategoryName AS Text , CategoryNo AS Value FROM Categorys ";
            str_query += GetSQLWhere();
            str_query += "ORDER BY CategoryNo";
            var model = dpr.ReadAll<SelectListItem>(str_query);
            return model;
        }

        public List<Categorys> GetTopCategoryList()
        {
            using var dpr = new DapperRepository();
            string str_query = @"
SELECT  Id, ParentNo, CategoryNo, CategoryName, Remark
FROM Categorys WHERE ParentNo = '' ORDER BY SortNo , CategoryNo
        ";
            return dpr.ReadAll<Categorys>(str_query);
        }
        public List<Categorys> GetDetailCategoryList(string parentNo)
        {
            using var dpr = new DapperRepository();
            string str_query = @"
SELECT  Id, ParentNo, CategoryNo, CategoryName, Remark
FROM Categorys WHERE ParentNo = @ParentNo ORDER BY SortNo , CategoryNo
        ";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("ParentNo", parentNo);
            return dpr.ReadAll<Categorys>(str_query, parm);
        }
        /// <summary>
        /// 取得單筆資料(同步呼叫)
        /// </summary>
        /// <param name="categoryNo">分類代號</param>
        /// <returns></returns>
        public void SetCategoryName(string categoryNo)
        {
            if (!string.IsNullOrEmpty(SessionService.SearchText))
            {
                SessionService.CategoryNo = "";
                SessionService.CategoryName = $"搜尋商品：{SessionService.SearchText}";
                SessionService.CategoryImage = $"~/images/category/all.jpg";
                return;
            }
            if (string.IsNullOrEmpty(categoryNo))
            {
                SessionService.CategoryNo = "";
                SessionService.CategoryName = "全站商品";
                SessionService.CategoryImage = $"~/images/category/all.jpg";
                return;
            }
            var model = new Categorys();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            sql_query += " WHERE Categorys.CategoryNo = @CategoryNo ";
            DynamicParameters parm = new DynamicParameters();
            parm.Add("CategoryNo", categoryNo);

            model = dpr.ReadSingle<Categorys>(sql_query, parm);
            SessionService.CategoryNo = categoryNo;
            SessionService.CategoryName = model.CategoryName;

            DynamicParameters parm1 = new DynamicParameters();
            parm1.Add("CategoryNo", model.ParentNo);
            var model1 = dpr.ReadSingle<Categorys>(sql_query, parm1);
            if (model1 != null)
            {
                SessionService.CategoryName = $"{model1.CategoryName} / {model.CategoryName}";
                SessionService.CategoryImage = $"~/images/category/{model1.CategoryNo}.jpg";
            }
            else
            {
                SessionService.CategoryImage = "~/images/category/man.jpg";
            }
        }

        /// <summary>
        /// 取得單筆資料(同步呼叫)
        /// </summary>
        /// <param name="categoryNo">分類代號</param>
        /// <returns></returns>
        public Categorys GetData(string categoryNo)
        {
            var model = new Categorys();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            sql_query += "WHERE CategoryNo = @CategoryNo ";
            sql_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            //自定義的 Weher Parm 參數
            parm.Add("CategoryNo", categoryNo);
            model = dpr.ReadSingle<Categorys>(sql_query, parm);
            return model;
        }
    }
}
