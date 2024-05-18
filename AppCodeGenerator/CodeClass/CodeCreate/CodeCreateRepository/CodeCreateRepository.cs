using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public string CreateRepository(CodeGeneratorModel model)
        {
            string str_message = "";
            string path = Directory.GetCurrentDirectory();
            string pathMetaName = "";
            string fileName = "";
            string tableName = "";
            string nameSpace = "";

            path += $"\\Models\\RepositoryModel";
            model.ModelNameSpace = nameSpace;

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using var codeService = new CodeGeneratorService();
            var tables = new List<CodeTableViewModel>();
            if (model.SelectAll)
                tables = codeService.GetTableViewList(model.ConnStringName, false);
            else if (model.TableSelectType == "1")
                tables.Add(new CodeTableViewModel() { Id = 1, Name = model.TableViewName, TypeName = "Table" });
            else
            {
                foreach (var table in model.TableViews)
                {
                    tables.Add(new CodeTableViewModel()
                    {
                        Name = table
                    });
                }
            }

            using var dpr = new DapperRepository();
            foreach (var item in tables)
            {
                List<string> lines = new List<string>();
                tableName = item.Name;
                fileName = $"repo{tableName}.cs";

                pathMetaName = Path.Combine(path, fileName);
                if (File.Exists(pathMetaName))
                {
                    if (!model.ForceOverride)
                    {
                        if (model.TableSelectType == "1")
                        {
                            str_message = "檔案已存在，因設置不進行覆蓋，因此未進行重建檔案!!";
                            break;
                        }
                        continue;
                    }
                    File.Delete(pathMetaName);
                }

                lines.Add("/// <summary>");
                lines.Add($"/// {tableName} CRUD 程式");
                lines.Add("/// </summary>");
                lines.Add($"public class z_repo{tableName} : BaseClass");
                lines.Add("{");
                lines.Add("    #region SQL 指令設定區");
                lines.Add("    /// <summary>");
                lines.Add("    /// SQL 查詢欄位及表格指令");
                lines.Add("    /// </summary>");
                lines.Add("    /// <returns></returns>");
                lines.Add("    public string GetSQLSelect()");
                lines.Add("    {");
                if (model.UseSQLSelect && !string.IsNullOrEmpty(model.SQLSelectSyntax))
                {
                    List<string> syntax = new List<string>();
                    syntax = model.SQLSelectSyntax.Split('\n').ToList();
                    lines.Add("        string str_query = @\"");
                    foreach (string line in syntax)
                    {
                        var data = line.Replace("\r", "");
                        if (!string.IsNullOrEmpty(data))
                        {
                            if (data.Substring((data.Length - 1), 1) != " ") data += " ";
                        }
                        lines.Add(data);
                    }
                    lines.Add("\";");
                    lines.Add("        //自動由表格 Class 產生 SQL 查詢指令");
                    lines.Add("        //using var dpr = new DapperRepository();");
                    lines.Add($"        //str_query = dpr.GetSQLSelectCommand(new {tableName}());");
                }
                else
                {
                    lines.Add("        string str_query = \"\";");
                    lines.Add("        //自動由表格 Class 產生 SQL 查詢指令");
                    lines.Add("        using var dpr = new DapperRepository();");
                    lines.Add($"        str_query = dpr.GetSQLSelectCommand(new {tableName}());");

                    var entity = $"{AppService.ProjectName}.Models.{tableName}";
                    var str_query = dpr.GetSQLSelectCommandList(entity, 100);
                    if (str_query.Count() > 0)
                    {
                        foreach (var row in str_query)
                        {
                            lines.Add($"        //{row}");
                        }
                    }
                }
                lines.Add("        return str_query;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 取得模擬搜尋的欄位集合");
                lines.Add("    /// </summary>");
                lines.Add("    /// <returns></returns>");
                lines.Add("    public List<string> GetSearchColumns()");
                lines.Add("    {");
                lines.Add("        List<string> searchColumn = new List<string>();");
                lines.Add("        //使用者自定義的可搜尋欄位(文字型態欄位)");
                if (model.UseSQLSelect && !string.IsNullOrEmpty(model.SQLSelectSyntax))
                {
                    var datas = codeService.GetTableModelFromSQLSelect(model.SQLSelectSyntax);
                    foreach (var data in datas)
                    {
                        lines.Add($"        searchColumn.Add(\"{data.ColumnName}\");");
                    }
                    lines.Add("");
                    lines.Add("        //由系統自動取得文字欄位的集合");
                    lines.Add("        //using var dpr = new DapperRepository();");
                    lines.Add($"        //searchColumn = dpr.GetStringColumnList(new {tableName}());");
                }
                else
                {
                    lines.Add("        //searchColumn.Add(\"欄位名稱\");");
                    lines.Add("");
                    lines.Add("        //由系統自動取得文字欄位的集合");
                    lines.Add("        using var dpr = new DapperRepository();");
                    lines.Add($"        searchColumn = dpr.GetStringColumnList(new {tableName}());");

                    var entity = $"{AppService.ProjectName}.Models.{tableName}";
                    var str_query = dpr.GetStringColumnList(entity, 100);
                    if (str_query.Count() > 0)
                    {
                        foreach (var row in str_query)
                        {
                            lines.Add($"        //{row}");
                        }
                    }
                }
                lines.Add("        return searchColumn;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// SQL 查詢條件式指令");
                lines.Add("    /// </summary>");
                lines.Add("    /// <returns></returns>");
                lines.Add("    public string GetSQLWhere()");
                lines.Add("    {");
                lines.Add("        string str_query = \"\";");
                lines.Add("        return str_query;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 預設 SQL 排序指令");
                lines.Add("    /// </summary>");
                lines.Add($"    private readonly string DefaultOrderByColumn = \"{model.SortColumnName}\";");
                lines.Add("    /// <summary>");
                lines.Add("    /// 預設 SQL 排序方式指令");
                lines.Add("    /// </summary>");
                lines.Add($"    private readonly string DefaultOrderByDirection = \"{model.SortDirection}\";");
                lines.Add("    /// <summary>");
                lines.Add("    /// SQL 查詢排序指令");
                lines.Add("    /// </summary>");
                lines.Add("    /// <returns></returns>");
                lines.Add("    public string GetSQLOrderBy()");
                lines.Add("    {");
                lines.Add("        if (string.IsNullOrEmpty(OrderByColumn)) OrderByColumn = DefaultOrderByColumn;");
                lines.Add("        if (string.IsNullOrEmpty(OrderByDirection)) OrderByColumn = DefaultOrderByDirection;");
                lines.Add("        string str_query = $\" ORDER BY {OrderByColumn}\";");
                lines.Add("        if (!string.IsNullOrEmpty(OrderByDirection)) str_query += $\" {OrderByDirection}\";");
                lines.Add("        return str_query;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 取得下拉式選單資料集");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"showNo\">是否顯示編號</param>");
                lines.Add("    /// <returns></returns>");
                lines.Add("    public List<SelectListItem> GetDropDownList(bool showNo = true)");
                lines.Add("    {");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add("        string str_query = \"SELECT \";");
                if (model.SelectAll || model.TableSelectType == "2")
                {
                    lines.Add("        if (showNo) str_query += \"DataNo + ' ' + \";");
                    lines.Add($"        str_query += \"DataName AS Text , DataNo AS Value FROM {tableName} ORDER BY DataNo\";");
                }
                else
                {
                    lines.Add($"        if (showNo) str_query += \"{model.DropDownColumnNo} + ' ' + \";");
                    lines.Add($"        str_query += \"{model.DropDownColumnName} AS Text , {model.DropDownColumnNo} AS Value FROM {tableName} ORDER BY {model.DropDownColumnNo}\";");
                }
                lines.Add("        var model = dpr.ReadAll<SelectListItem>(str_query);");
                lines.Add("        return model;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// SQL 新增指令");
                lines.Add("    /// </summary>");
                lines.Add("    /// <returns></returns>");
                lines.Add("    public string GetSQLInsert()");
                lines.Add("    {");
                lines.Add("        string str_query = \"\";");
                lines.Add("        //自動由表格 Class 產生 Insert 查詢指令");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add($"        str_query = dpr.GetSQLInsertCommand(new {tableName}());");
                lines.Add("        return str_query;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// SQL 刪除指令");
                lines.Add("    /// </summary>");
                lines.Add("    /// <returns></returns>");
                lines.Add("    public string GetSQLDelete()");
                lines.Add("    {");
                lines.Add("        string str_query = \"\";");
                lines.Add("        //自動由表格 Class 產生 Delete 查詢指令");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add($"        str_query = dpr.GetSQLDeleteCommand(new {tableName}());");
                lines.Add("        return str_query;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// SQL 修改指令");
                lines.Add("    /// </summary>");
                lines.Add("    /// <returns></returns>");
                lines.Add("    public string GetSQLUpdate()");
                lines.Add("    {");
                lines.Add("        string str_query = \"\";");
                lines.Add("        //自動由表格 Class 產生 Update 查詢指令");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add($"        str_query = dpr.GetSQLUpdateCommand(new {tableName}());");
                lines.Add("        return str_query;");
                lines.Add("    }");
                lines.Add("    #endregion");
                lines.Add("    #region 物件建構子");
                lines.Add("    /// <summary>");
                lines.Add("    /// OrderBy 排序指令");
                lines.Add("    /// </summary>");
                lines.Add("    /// <value></value>");
                lines.Add("    public string OrderByColumn { get; set; }");
                lines.Add("    /// <summary>");
                lines.Add("    /// OrderBy 排序方式");
                lines.Add("    /// </summary>");
                lines.Add("    public string OrderByDirection { get; set; }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 建構子");
                lines.Add("    /// </summary>");
                lines.Add($"    public z_repo{tableName}()");
                lines.Add("    {");
                lines.Add("        OrderByColumn = DefaultOrderByColumn;");
                lines.Add("        OrderByDirection = DefaultOrderByDirection;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 建構子");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"orderByColumn\">排序欄位</param>");
                lines.Add("    /// <param name=\"orderByDirection\">排序方式</param>");
                lines.Add($"    public z_repo{tableName}(string orderByColumn, string orderByDirection)");
                lines.Add("    {");
                lines.Add("        if (!string.IsNullOrEmpty(orderByColumn))");
                lines.Add("            OrderByColumn = orderByColumn;");
                lines.Add("        else");
                lines.Add("            OrderByColumn = DefaultOrderByColumn;");
                lines.Add("        if (!string.IsNullOrEmpty(orderByDirection))");
                lines.Add("            OrderByDirection = orderByDirection;");
                lines.Add("        else");
                lines.Add("            OrderByDirection = DefaultOrderByDirection;");
                lines.Add("    }");
                lines.Add("    #endregion");
                lines.Add("    #region 資料表 CRUD 指令(使用同步呼叫)");
                lines.Add("    /// <summary>");
                lines.Add("    /// 取得單筆資料(同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"id\">Key 欄位值</param>");
                lines.Add("    /// <returns></returns>");
                lines.Add($"    public {tableName} GetData(int id)");
                lines.Add("    {");
                lines.Add($"        var model = new {tableName}();");
                lines.Add("        if (id == 0)");
                lines.Add("        {");
                lines.Add("            //新增預設值");
                lines.Add("        }");
                lines.Add("        else");
                lines.Add("        {");
                lines.Add("            using var dpr = new DapperRepository();");
                lines.Add("            string sql_query = GetSQLSelect();");
                lines.Add("            string sql_where = GetSQLWhere();");
                lines.Add("            sql_query += dpr.GetSQLSelectWhereById(model, sql_where);");
                lines.Add("            sql_query += GetSQLOrderBy();");
                lines.Add("            DynamicParameters parm = dpr.GetSQLSelectKeyParm(model, id);");
                lines.Add("            if (!string.IsNullOrEmpty(sql_where))");
                lines.Add("            {");
                lines.Add("                //自定義的 Weher Parm 參數");
                lines.Add("                //parm.Add(\"參數名稱\", \"參數值\");");
                lines.Add("            }");
                lines.Add($"            model = dpr.ReadSingle<{tableName}>(sql_query, parm);");
                lines.Add("        }");
                lines.Add("        return model;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 取得多筆資料(同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"searchString\">模糊搜尋文字(空白或不傳入表示不搜尋)</param>");
                lines.Add("    /// <returns></returns>");
                lines.Add($"    public List<{tableName}> GetDataList(string searchString = \"\")");
                lines.Add("    {");
                lines.Add("        List<string> searchColumns = GetSearchColumns();");
                lines.Add("        DynamicParameters parm = new DynamicParameters();");
                lines.Add($"        var model = new List<{tableName}>();");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add("        string sql_query = GetSQLSelect();");
                lines.Add("        string sql_where = GetSQLWhere();");
                lines.Add("        sql_query += sql_where;");
                lines.Add("        if (!string.IsNullOrEmpty(searchString))");
                lines.Add($"            sql_query += dpr.GetSQLWhereBySearchColumn(new {tableName}(), searchColumns, sql_where, searchString);");
                lines.Add("        if (!string.IsNullOrEmpty(sql_where))");
                lines.Add("        {");
                lines.Add("            //自定義的 Weher Parm 參數");
                lines.Add("            //parm.Add(\"參數名稱\", \"參數值\");");
                lines.Add("        }");
                lines.Add("        sql_query += GetSQLOrderBy();");
                lines.Add($"        model = dpr.ReadAll<{tableName}>(sql_query, parm);");
                lines.Add("        return model;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 新增或修改資料(同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"model\">資料模型</param>");
                lines.Add($"    public void CreateEdit({tableName} model)");
                lines.Add("    {");
                lines.Add("        if (model.Id == 0)");
                lines.Add("            Create(model);");
                lines.Add("        else");
                lines.Add("            Edit(model);");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 新增資料(同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"model\">資料模型</param>");
                lines.Add($"    public void Create({tableName} model)");
                lines.Add("    {");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add("        string str_query = dpr.GetSQLInsertCommand(model);");
                lines.Add("        DynamicParameters parm = dpr.GetSQLInsertParameters(model);");
                lines.Add("        dpr.Execute(str_query, parm);");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 更新資料(同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"model\">資料模型</param>");
                lines.Add($"    public void Edit({tableName} model)");
                lines.Add("    {");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add("        string str_query = dpr.GetSQLUpdateCommand(model);");
                lines.Add("        DynamicParameters parm = dpr.GetSQLUpdateParameters(model);");
                lines.Add("        dpr.Execute(str_query, parm);");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 刪除資料(同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"id\">Id</param>");
                lines.Add("    public void Delete(int id = 0)");
                lines.Add("    {");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add($"        string str_query = dpr.GetSQLDeleteCommand(new {tableName}());");
                lines.Add($"        DynamicParameters parm = dpr.GetSQLDeleteParameters(new {tableName}(), id);");
                lines.Add("        dpr.Execute(str_query, parm);");
                lines.Add("    }");
                lines.Add("    #endregion");
                lines.Add("    #region 資料表 CRUD 指令(使用非同步呼叫)");
                lines.Add("    /// <summary>");
                lines.Add("    /// 取得單筆資料(非同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"id\">Key 欄位值</param>");
                lines.Add("    /// <returns></returns>");
                lines.Add($"    public async Task<{tableName}> GetDataAsync(int id)");
                lines.Add("    {");
                lines.Add($"        var model = new {tableName}();");
                lines.Add("        if (id == 0)");
                lines.Add("        {");
                lines.Add("            //新增預設值");
                lines.Add("        }");
                lines.Add("        else");
                lines.Add("        {");
                lines.Add("            using var dpr = new DapperRepository();");
                lines.Add("            string sql_query = GetSQLSelect();");
                lines.Add("            string sql_where = GetSQLWhere();");
                lines.Add("            sql_query += dpr.GetSQLSelectWhereById(model, sql_where);");
                lines.Add("            sql_query += GetSQLOrderBy();");
                lines.Add("            DynamicParameters parm = dpr.GetSQLSelectKeyParm(model, id);");
                lines.Add("            if (!string.IsNullOrEmpty(sql_where))");
                lines.Add("            {");
                lines.Add("                //自定義的 Weher Parm 參數");
                lines.Add("                //parm.Add(\"參數名稱\", \"參數值\");");
                lines.Add("            }");
                lines.Add($"            model = await dpr.ReadSingleAsync<{tableName}>(sql_query, parm);");
                lines.Add("        }");
                lines.Add("        return model;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 取得多筆資料(非同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"searchString\">模糊搜尋文字(空白或不傳入表示不搜尋)</param>");
                lines.Add("    /// <returns></returns>");
                lines.Add($"    public async Task<List<{tableName}>> GetDataListAsync(string searchString = \"\")");
                lines.Add("    {");
                lines.Add("        List<string> searchColumns = GetSearchColumns();");
                lines.Add("        DynamicParameters parm = new DynamicParameters();");
                lines.Add($"        var model = new List<{tableName}>();");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add("        string sql_query = GetSQLSelect();");
                lines.Add("        string sql_where = GetSQLWhere();");
                lines.Add("       sql_query += sql_where;");
                lines.Add("        if (!string.IsNullOrEmpty(searchString))");
                lines.Add($"           sql_query += dpr.GetSQLWhereBySearchColumn(new {tableName}(), searchColumns, sql_where, searchString);");
                lines.Add("        if (!string.IsNullOrEmpty(sql_where))");
                lines.Add("        {");
                lines.Add("            //自定義的 Weher Parm 參數");
                lines.Add("            //parm.Add(\"參數名稱\", \"參數值\");");
                lines.Add("        }");
                lines.Add("        sql_query += GetSQLOrderBy();");
                lines.Add($"        model = await dpr.ReadAllAsync<{tableName}>(sql_query, parm);");
                lines.Add("        return model;");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 新增或修改資料(非同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"model\">資料模型</param>");
                lines.Add("    /// <returns></returns>");
                lines.Add($"    public async Task CreateEditAsync({tableName} model)");
                lines.Add("    {");
                lines.Add("        if (model.Id == 0)");
                lines.Add("            await CreateAsync(model);");
                lines.Add("        else");
                lines.Add("            await EditAsync(model);");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 新增資料(非同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"model\">資料模型</param>");
                lines.Add("    /// <returns></returns>");
                lines.Add($"    public async Task CreateAsync({tableName} model)");
                lines.Add("    {");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add("        string str_query = dpr.GetSQLInsertCommand(model);");
                lines.Add("        DynamicParameters parm = dpr.GetSQLInsertParameters(model);");
                lines.Add("        await dpr.ExecuteAsync(str_query, parm);");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 更新資料(非同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"model\">資料模型</param>");
                lines.Add("    /// <returns></returns>");
                lines.Add($"    public async Task EditAsync({tableName} model)");
                lines.Add("    {");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add("        string str_query = dpr.GetSQLUpdateCommand(model);");
                lines.Add("        DynamicParameters parm = dpr.GetSQLUpdateParameters(model);");
                lines.Add("        await dpr.ExecuteAsync(str_query, parm);");
                lines.Add("    }");
                lines.Add("    /// <summary>");
                lines.Add("    /// 刪除資料(非同步呼叫)");
                lines.Add("    /// </summary>");
                lines.Add("    /// <param name=\"id\">Id</param>");
                lines.Add("    /// <returns></returns>");
                lines.Add("    public async Task DeleteAsync(int id = 0)");
                lines.Add("    {");
                lines.Add("        using var dpr = new DapperRepository();");
                lines.Add($"        string str_query = dpr.GetSQLDeleteCommand(new {tableName}());");
                lines.Add($"        DynamicParameters parm = dpr.GetSQLDeleteParameters(new {tableName}(), id);");
                lines.Add("        await dpr.ExecuteAsync(str_query, parm);");
                lines.Add("    }");
                lines.Add("    #endregion");
                lines.Add("    #region 其它自定義事件與函數");
                lines.Add("    #endregion");
                lines.Add("}");

                try
                {
                    CreateFile(lines, path, fileName);
                    CodeSessionService.Affecteds += 1;
                }
                catch (Exception ex)
                {
                    str_message = ex.Message;
                }
            }
            if (CodeSessionService.Affecteds == 0 && !model.ForceOverride)
            {
                str_message = "因所有檔案已存在，且設置為不進行覆蓋，故未進行重建檔案!!";
            }
            return str_message;
        }
    }
}