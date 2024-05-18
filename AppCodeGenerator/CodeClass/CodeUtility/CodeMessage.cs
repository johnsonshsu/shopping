using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class CodeMessage : CodeBaseClass
    {
        public string CreateAreaMessage(CodeGeneratorModel model)
        {
            string str_message = "";
            str_message += "<div class=\"text-danger  py-3\">";
            str_message += $"區域名稱： Areas\\{model.AreaName} 已成功建立!!";
            str_message += $"建立區域數：共 {CodeSessionService.Affecteds} 個";
            str_message += "</div>";
            str_message += "<hr />";
            str_message += "<div class=\"fw-bold mb-2\">";
            str_message += "終端機指令語法：";
            str_message += "</div>";
            str_message += "<div class=\"bg-light py-3 px-2\">";
            str_message += $"dotnet aspnet-codegenerator area {model.AreaName}";
            str_message += "</div>";
            str_message += "<hr />";
            str_message += $"請注意要在 <b>Program.cs</b> 中加入以下區域的路由</br></br>";
            str_message += "<pre><code>";
            str_message += "app.MapControllerRoute(</br>";
            str_message += $"    name: \"{model.AreaName}Area\",</br>";
            str_message += "    pattern: \"{area:exists}/{controller=Home}/{action=Index}/{id?}\");</br>";
            str_message += "</code></pre>";
            return str_message;
        }
        public string CreateControllerMessage(CodeGeneratorModel model, bool isExists)
        {
            string str_folder = (string.IsNullOrEmpty(model.AreaName)) ? "Controllers" : $"Areas\\{model.AreaName}\\Controllers";
            string str_message = "";
            str_message += "<div class=\"text-danger  py-3\">";
            if (isExists)
            {
                str_message += $"控制器名稱： {str_folder}\\{model.ControllerName}Controller.cs 已存在, ";
                if (model.ForceOverride)
                    str_message += "並且已重新覆寫檔案!!";
                else
                    str_message += "無法重新建立!!";
            }
            else
            {
                str_message += $"控制器名稱： {str_folder}\\{model.ControllerName}Controller.cs 已成功建立!!";

            }
            str_message += $"建立控制器數：共 {CodeSessionService.Affecteds} 個";
            str_message += "</div>";
            str_message += "<hr />";
            str_message += "<div class=\"fw-bold mb-2\">";
            str_message += "終端機指令語法：";
            str_message += "</div>";
            str_message += "<div class=\"bg-light py-3 px-2\">";
            str_message += $"dotnet aspnet-codegenerator controller -name {model.ControllerName}Controller -async -outDir {str_folder}";
            if (model.ForceOverride) str_message += " --force";
            str_message += "</div>";
            return str_message;
        }
        public string CreateViewMessage(CodeGeneratorModel model, bool isExists)
        {
            string str_model_name = model.ModelName;
            string str_folder = (string.IsNullOrEmpty(model.AreaName)) ? $"Views\\{model.ControllerName}" : $"Areas\\{model.AreaName}\\Views\\{model.ControllerName}";
            string str_message = "";
            str_message += "<div class=\"text-danger py-3\">";
            if (isExists)
            {
                str_message += $"網頁(View)名稱： {str_folder}\\{model.ViewName}.cshtml 已存在, ";
                if (model.ForceOverride)
                    str_message += "並且已重新覆寫檔案!!";
                else
                    str_message += "無法重新建立!!";
            }
            else
            {
                str_message += $"網頁(View)名稱： {str_folder}\\{model.ViewName}.cshtml 已成功建立!!";
            }
            if (str_model_name.IndexOf(".") != -1)
            {
                var modelData = str_model_name.Split('.').ToList();
                foreach (var item in modelData)
                {
                    str_model_name = item;
                }
            }
            str_message += "</br>";
            str_message += $"建立網頁(View)數：共 {CodeSessionService.Affecteds} 個";
            str_message += "</div>";
            str_message += "<hr />";
            str_message += "<div class=\"fw-bold mb-2\">";
            str_message += "終端機指令語法：";
            str_message += "</div>";
            str_message += "<div class=\"bg-light py-3 px-2\">";
            str_message += $"dotnet aspnet-codegenerator view {model.ViewName} {model.TemplateName} -m {str_model_name} -l \"{model.LayoutName}\" -outDir {str_folder}";

            if (model.ForceOverride) str_message += " --force";
            str_message += "</div>";
            if (model.DropDownColumns != null && model.DropDownColumns.Count() > 0)
            {
                str_message += "<div class=\"text-danger my-3\">";
                str_message += $"請注意要在 <b>{model.ViewName}.cshtml</b> 中要手動設定下拉選單的資料來源!!";
                str_message += "</div>";
                str_message += "<pre class=\"bg-light\"><code>";
                foreach (var item in model.DropDownColumns)
                {
                    str_message += $"    //using var repo{item} = new z_repoTableName();</br>";
                }
                foreach (var item in model.DropDownColumns)
                {
                    str_message += $"    //var {item}List = repo{item}.GetDropDownList(true);</br>";
                }
                str_message += "</code></pre>";
                str_message += "<div class=\"text-danger my-3\">";
                str_message += $"另外在主版頁面中需要設定加入 !!";
                str_message += "</div>";
                str_message += "<pre class=\"bg-light\"><code>";
                foreach (var item in model.DropDownColumns)
                {
                    str_message += $"    //using var repo{item} = new z_repoTableName();</br>";
                }
                foreach (var item in model.DropDownColumns)
                {
                    str_message += $"    //var {item}List = repo{item}.GetDropDownList(true);</br>";
                }
                str_message += "</code></pre>";
            }

            return str_message;
        }
        public string CreateDbContextMessage(CodeGeneratorModel model, bool isExists)
        {
            string str_message = "";
            str_message += "<div class=\"text-danger py-3\">";
            str_message += $"資料庫模型： Models\\{model.DbContextName} 已成功建立!!";
            str_message += $"建立資料庫模型數：共 {CodeSessionService.Affecteds} 個";
            str_message += "</div>";
            str_message += "<hr />";
            str_message += "<div class=\"fw-bold mb-2\">";
            str_message += "終端機指令語法：";
            str_message += "</div>";
            str_message += "<div class=\"bg-light py-3 px-2\">";
            str_message += "dotnet ef dbcontext scaffold \"Name=ConnectionStrings:" + model.ConnStringName + "\" Microsoft.EntityFrameworkCore.SqlServer -n " + model.ProjectName + ".Models -o Models\\Tables --context-dir Models -c dbEntities -f --use-database-names --no-pluralize";
            str_message += "</div>";
            str_message += "<hr />";
            str_message += $"請注意要在 <b>Program.cs</b> 中加入以下區域的路由</br></br>";
            str_message += "<pre><code>";
            str_message += "app.MapControllerRoute(</br>";
            str_message += $"    name: \"{model.AreaName}Area\",</br>";
            str_message += "    pattern: \"{area:exists}/{controller=Home}/{action=Index}/{id?}\");</br>";
            str_message += "</code></pre>";
            return str_message;
        }

        public string CreateMetaDataMessage(CodeGeneratorModel model, bool isExists)
        {
            string str_message = "";
            str_message += "<div class=\"text-danger py-3\">";
            str_message += $"資料表欄位定義檔 (MetaData) 已成功建立!!";
            str_message += $"建立資料表欄位定義檔數：共 {CodeSessionService.Affecteds} 個";
            str_message += "</div>";
            str_message += "<hr />";
            str_message += "新建立的 MetaData 檔案需要檢查以下設定，例如：</br></br>";
            str_message += "<pre><code>";
            str_message += "[Display(Name = \"欄位名稱\")]</br>";
            str_message += "</code></pre>";
            return str_message;
        }

        public string CreateRepositoryMessage(CodeGeneratorModel model, bool isExists)
        {
            string str_message = "";
            str_message += "<div class=\"text-danger py-3\">";
            str_message += $"資料表 CRUD (Repository) 已成功建立!!";
            str_message += $"建立 Repository 檔數：共 {CodeSessionService.Affecteds} 個";
            str_message += "</div>";
            str_message += "<hr />";
            str_message += "新建立的 Repository 檔案需要檢查以下設定，例如：";
            str_message += "<pre><code></br>";
            str_message += "------------------------------------------------------------------</br></br>";
            str_message += "/// 預設 SQL 排序指令</br>";
            str_message += "private readonly string DefaultOrderByColumn = \"AboutUs.DataNo\";</br></br>";
            str_message += "------------------------------------------------------------------</br></br>";
            str_message += "/// SQL 查詢排序指令</br>";
            str_message += "public string GetSQLOrderBy()</br>";
            str_message += "{</br>";
            str_message += "    using var dpr = new DapperRepository();";
            str_message += "    string str_query = \"SELECT \";</br>";
            str_message += "    if (showNo) str_query += \"DataNo + ' ' + \";</br>";
            str_message += "    str_query += \"DataName AS Text , DataNo AS Value FROM AboutUs ORDER BY DataNo\";</br>";
            str_message += "    var model = dpr.ReadAll<SelectListItem>(str_query);</br>";
            str_message += "    return model;</br>";
            str_message += "}</br>";
            str_message += "</code></pre></br>";
            return str_message;
        }
    }
}