using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public string CreateTablesFile(CodeGeneratorModel model)
        {
            string str_message = "";
            string path = Directory.GetCurrentDirectory();
            string pathTablesName = "";
            string fileName = "";
            string tableType = "";
            string nameSpace = model.ProjectName;
            string str_line = "";

            path += $"\\Models\\Tables";
            nameSpace = $"{model.ProjectName}.Models";
            model.ModelNameSpace = nameSpace;

            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            using var codeService = new CodeGeneratorService();
            var tables = new List<CodeTableViewModel>();
            if (model.SelectAll)
                tables = codeService.GetTableViewList(model.ConnStringName, model.CreateView);
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
            int int_index = 0;
            foreach (var table in tables)
            {
                int_index++;
                table.Id = int_index;
                table.TypeName = codeService.GetTableType(table.Name);
            }

            using var dpr = new DapperRepository();
            string str_query = "";
            foreach (var item in tables)
            {
                fileName = $"{item.Name}.cs";
                if (item.TypeName == "Table")
                    tableType = "sys.tables";
                else
                    tableType = "sys.views";
                pathTablesName = Path.Combine(path, fileName);
                if (File.Exists(pathTablesName))
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
                    File.Delete(pathTablesName);
                }
                List<string> lines = new List<string>();

                str_query = @"
SELECT 
tab.name AS TableName, 
col.colid AS ColumnId,
col.name AS ColumnName, 
tpe.name AS DataType, 
col.length AS DataTypeLength, 
(CASE col.isnullable WHEN '1' THEN 'YES' WHEN '0' THEN 'NO' END) 
AS IsNullable ";
                str_query += $"FROM {tableType} AS tab ";
                str_query += @"LEFT OUTER JOIN sys.syscolumns AS col ON tab.object_id = col.id 
LEFT OUTER JOIN sys.systypes AS tpe ON col.xusertype = tpe.xusertype 
WHERE tab.name = ";
                string str_sql = $"{str_query} '{item.Name}' ORDER BY col.colid";
                var cols = dpr.ReadAll<CodeTableModel>(str_sql);
                if (cols != null && cols.Count() > 0)
                {
                    lines.Add("using System;");
                    lines.Add("using System.Collections.Generic;");
                    lines.Add("");
                    lines.Add($"namespace {nameSpace}");
                    lines.Add("{");
                    lines.Add($"    public partial class {item.Name}");
                    lines.Add("    {");
                    foreach (var col in cols)
                    {
                        str_line = "        public ";
                        str_line += codeService.GetPropertyType(col);
                        str_line += $" {col.ColumnName} ";
                        str_line += "{ get; set; }";
                        lines.Add(str_line);
                    }
                    lines.Add("    }");
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
            }
            if (CodeSessionService.Affecteds == 0 && !model.ForceOverride)
            {
                str_message = "因所有檔案已存在，且設置為不進行覆蓋，故未進行重建檔案!!";
            }
            return str_message;
        }
    }
}