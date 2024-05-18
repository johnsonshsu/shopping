using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class CodeGeneratorModel
    {
        [Display(Name = "產生類別")]
        public string GeneratorType { get; set; } = "";
        [Display(Name = "範本名稱")]
        public string TemplateName { get; set; } = "";
        [Display(Name = "專案名稱")]
        public string ProjectName { get; set; } = "";
        [Display(Name = "區域名稱")]
        public string AreaName { get; set; } = "";
        [Display(Name = "控制器名稱")]
        public string ControllerName { get; set; } = "";
        [Display(Name = "網頁(View)名稱")]
        public string ViewName { get; set; } = "";
        [Display(Name = "資料模型名稱")]
        public string ModelName { get; set; } = "";
        [Display(Name = "資料模型命名空間")]
        public string ModelNameSpace { get; set; } = "";
        [Display(Name = "資料模型類別名稱")]
        public string ModelClassName { get; set; } = "";
        [Display(Name = "主版頁面名稱")]
        public string LayoutName { get; set; } = "";
        [Display(Name = "資料庫模型名稱")]
        public string DbContextName { get; set; } = "";
        [Display(Name = "產生檔案類別")]
        public string DBContextType { get; set; } = "";
        [Display(Name = "連線字串名稱")]
        public string ConnStringName { get; set; } = "";
        [Display(Name = "連線字串")]
        public string ConnectionString { get; set; } = "";
        [Display(Name = "刪除提示編號欄位")]
        public string DeleteNo { get; set; } = "";
        [Display(Name = "刪除提示名稱欄位")]
        public string DeleteName { get; set; } = "";
        [Display(Name = "主索引鍵名稱")]
        public string KeyName { get; set; } = "";
        [Display(Name = "Card 大小")]
        public string CardSize { get; set; } = "";
        [Display(Name = "標題與編輯欄位比例")]
        public string LabelScaleSizeName { get; set; } = "2 : 10";
        [Display(Name = "表格選取方式")]
        public string TableSelectType { get; set; } = "1";
        [Display(Name = "強制檔案覆寫")]
        public bool ForceOverride { get; set; } = true;
        [Display(Name = "傳入資料模型")]
        public bool UseModel { get; set; } = true;
        [Display(Name = "使用 Card 功能")]
        public bool UseCard { get; set; } = false;
        [Display(Name = "繼承 BaseController")]
        public bool BaseController { get; set; } = false;
        [Display(Name = "產生 View 結構")]
        public bool CreateView { get; set; } = false;
        [Display(Name = "選取全部表格")]
        public bool SelectAll { get; set; } = false;
        [Display(Name = "選取全部欄位")]
        public bool SelectAllColumn { get; set; } = false;
        [Display(Name = "自定義 SELECT 語法")]
        public bool UseSQLSelect { get; set; } = true;
        [Display(Name = "產生下拉選單資料")]
        public bool UseDropDownList { get; set; } = true;
        [Display(Name = "編輯版面欄位數")]
        public int LayoutColCount { get; set; } = 1;
        [Display(Name = "標題欄位比例")]
        public int LabelScaleSize { get; set; } = 2;
        [Display(Name = "資料表及檢視")]
        public string TableViewName { get; set; } = "";
        [Display(Name = "SQL SELECT 語法")]
        public string SQLSelectSyntax { get; set; } = "";
        [Display(Name = "排序欄位")]
        public string SortColumnName { get; set; } = "";
        [Display(Name = "排序方式")]
        public string SortDirection { get; set; } = "ASC";
        [Display(Name = "下拉選單編號欄位")]
        public string DropDownColumnNo { get; set; } = "";
        [Display(Name = "下拉選單名稱欄位")]
        public string DropDownColumnName { get; set; } = "";
        [Display(Name = "資料表及檢視")]
        public IEnumerable<string> TableViews { get; set; } = new List<string>();
        [Display(Name = "顯示欄位複選")]
        public IEnumerable<string> DisplayColumns { get; set; } = new List<string>();
        [Display(Name = "下拉選單欄位複選")]
        public IEnumerable<string> DropDownColumns { get; set; } = new List<string>();

    }
}