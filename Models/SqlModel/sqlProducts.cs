using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shopping.Models
{
    public class z_sqlProducts : DapperSql<Products>
    {
        public z_sqlProducts()
        {
            OrderByColumn = SessionService.SortColumn;
            OrderByDirection = SessionService.SortDirection;
            DefaultOrderByColumn = "Products.ProdNo";
            DefaultOrderByDirection = "ASC";
            if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;
            if (string.IsNullOrEmpty(OrderByDirection)) OrderByDirection = DefaultOrderByDirection;
        }

        public override string GetSQLSelect()
        {
            string str_query = @"
SELECT Products.Id, Products.ProdNo, Products.ProdName, Products.CategoryNo, Categorys.CategoryName, 
Products.InventoryQty, Products.StatusNo, ProductStatus.StatusName, Products.SpecificationText, Products.ContentText, 
Products.SalePrice, Products.DiscountPrice, Products.Remark, 
CASE WHEN Products.DiscountPrice != '0' THEN Products.DiscountPrice ELSE Products.SalePrice END AS MinPrice 
FROM Products 
LEFT OUTER JOIN ProductStatus ON Products.StatusNo = ProductStatus.StatusNo 
LEFT OUTER JOIN Categorys ON Products.CategoryNo = Categorys.CategoryNo 
";
            return str_query;
        }

        public override List<string> GetSearchColumns()
        {
            //由系統自動取得文字欄位的集合
            List<string> searchColumn;
            searchColumn = dpr.GetStringColumnList(EntityObject);
            searchColumn.Add("Categorys.CategoryName");
            searchColumn.Add("ProductStatus.StatusName");
            return searchColumn;
        }

        /// <summary>
        /// 取得單筆資料(同步呼叫)
        /// </summary>
        /// <param name="prodNo">商品代號</param>
        /// <returns></returns>
        public Products GetData(string prodNo)
        {
            var model = new Products();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = "WHERE Products.ProdNo = @ProdNo ";
            sql_query += sql_where;
            sql_query += GetSQLOrderBy();
            DynamicParameters parm = new DynamicParameters();
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Where Parm 參數
                parm.Add("ProdNo", prodNo);
            }
            model = dpr.ReadSingle<Products>(sql_query, parm);
            return model;
        }

        /// <summary>
        /// 取得多筆資料(同步呼叫)
        /// </summary>
        /// <param name="sortNo">排序方式</param>
        /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
        /// <returns></returns>
        public List<Products> GetDataList(string sortNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Products>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = GetSQLWhere();
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(new Products(), searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Where Parm 參數
                //parm.Add("參數名稱", "參數值");
            }
            if (sortNo == "High") sql_query += " ORDER BY MinPrice DESC;";
            else if (sortNo == "Low") sql_query += " ORDER BY MinPrice ASC";
            else sql_query += " ORDER BY Products.ProdNo ASC";

            model = dpr.ReadAll<Products>(sql_query, parm);
            return model;
        }
        /// <summary>
        /// 取得多筆資料(同步呼叫)
        /// </summary>
        /// <param name="categoryNo">Category No</param>
        /// <param name="sortNo">排序方式</param>
        /// <param name="searchString">模糊搜尋文字(空白或不傳入表示不搜尋)</param>
        /// <returns></returns>
        public List<Products> GetDataList(string categoryNo, string sortNo, string searchString = "")
        {
            List<string> searchColumns = GetSearchColumns();
            DynamicParameters parm = new DynamicParameters();
            var model = new List<Products>();
            using var dpr = new DapperRepository();
            string sql_query = GetSQLSelect();
            string sql_where = " WHERE Products.CategoryNo = @CategoryNo ";
            sql_query += sql_where;
            if (!string.IsNullOrEmpty(searchString))
                sql_query += dpr.GetSQLWhereBySearchColumn(new Products(), searchColumns, sql_where, searchString);
            if (!string.IsNullOrEmpty(sql_where))
            {
                //自定義的 Where Parm 參數
                parm.Add("CategoryNo", categoryNo);
            }
            if (sortNo == "High") sql_query += " ORDER BY MinPrice DESC;";
            else if (sortNo == "Low") sql_query += " ORDER BY MinPrice ASC";
            else sql_query += " ORDER BY Products.ProdNo ASC";

            model = dpr.ReadAll<Products>(sql_query, parm);
            return model;
        }

        /// <summary>
        /// 取得指定分類商品資料(同步呼叫)
        /// </summary>
        /// <param name="categoryNo">分類代號</param>
        /// <returns></returns>
        public List<Products> GetCategoryDataList(string categoryNo = "All")
        {
            List<string> searchColumns = GetSearchColumns();
            var model = new List<Products>();
            using var dpr = new DapperRepository();
            using var cate = new z_sqlCategorys();
            DynamicParameters parm = new DynamicParameters();
            string sql_query = GetSQLSelect();
            if (!string.IsNullOrEmpty(SessionService.SearchText))
            {
                sql_query += $" WHERE (Products.ProdNo LIKE '%{SessionService.SearchText}%' ";
                sql_query += $" OR Products.ProdName LIKE '%{SessionService.SearchText}%') ";
                sql_query += " AND Products.IsEnabled = @IsEnabled";
                parm.Add("IsEnabled", true);
                model = dpr.ReadAll<Products>(sql_query, parm);
            }
            else
            {
                if (categoryNo != "All")
                {

                    var data = cate.GetData(categoryNo);
                    if (String.IsNullOrEmpty(data.ParentNo))
                    {
                        sql_query += " WHERE Categorys.ParentNo = @ParentNo AND Products.IsEnabled = @IsEnabled";
                        parm.Add("ParentNo", categoryNo);
                        parm.Add("IsEnabled", true);
                    }
                    else
                    {
                        sql_query += " WHERE Products.CategoryNo = @CategoryNo AND Products.IsEnabled = @IsEnabled";
                        parm.Add("CategoryNo", categoryNo);
                        parm.Add("IsEnabled", true);
                    }
                    sql_query += GetSortOrderBy();
                    model = dpr.ReadAll<Products>(sql_query, parm);
                }
                else
                {
                    sql_query += GetSortOrderBy();
                    model = dpr.ReadAll<Products>(sql_query);
                }
            }
            return model;
        }

        private string GetSortOrderBy()
        {
            if (SessionService.SortNo == "High") return " ORDER BY Products.CategoryNo , Products.SalePrice DESC";
            if (SessionService.SortNo == "Low") return " ORDER BY Products.CategoryNo , Products.SalePrice ASC";
            if (SessionService.SortNo == "Product") return " ORDER BY Products.CategoryNo , Products.ProdNo ASC";
            return " ORDER BY Products.ProdNo";
        }
    }
}