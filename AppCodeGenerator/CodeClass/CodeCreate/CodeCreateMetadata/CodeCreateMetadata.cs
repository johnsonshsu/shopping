using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public string CreateMetadata(CodeGeneratorModel model)
        {
            string str_message = "";
            string path = Directory.GetCurrentDirectory();
            string pathMetaName = "";
            string fileName = "";
            string colComments = "";
            string colType = "";
            string colNull = "";
            string tableName = "";
            string nameSpace = model.ProjectName;
            string str_line = "";

            path += $"\\Models\\MetadataModel";
            nameSpace = $"{model.ProjectName}.Models";
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
            string str_query = "";
            foreach (var item in tables)
            {
                List<string> lines = new List<string>();
                tableName = item.Name;
                fileName = $"meta{tableName}.cs";

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
                str_query = @"
SELECT  col.name AS ColumnName, typ.name AS DataType, col.prec AS DataTypePrec, 
col.scale AS DataTypeScale, CASE WHEN col.isnullable = 1 THEN 'Y' ELSE 'N' END AS IsNullable, 
CASE WHEN col.status = 128 THEN 'Y' ELSE 'N' END AS IsIdentity,
(SELECT value FROM sys.extended_properties
WHERE  (major_id = OBJECT_ID(tab.name)) AND (minor_id = col.colid)) AS Comments
FROM  sys.sysobjects AS tab 
INNER JOIN sys.syscolumns AS col 
LEFT OUTER JOIN sys.syscomments AS com 
INNER JOIN sys.sysobjects AS obj 
ON com.id = obj.id 
ON col.cdefault = com.id AND com.colid = 1 
ON tab.id = col.id 
INNER JOIN sys.systypes AS typ ON col.xusertype = typ.xusertype
WHERE (tab.xtype = 'U') AND tab.name = ";
                string str_sql = $"{str_query} '{tableName}' ORDER BY col.colid";
                var cols = dpr.ReadAll<CodeTableModel>(str_sql);
                if (cols != null && cols.Count() > 0)
                {
                    lines.Add("using System.ComponentModel.DataAnnotations;");
                    lines.Add("using System.ComponentModel.DataAnnotations.Schema;");
                    lines.Add("");
                    lines.Add($"namespace {nameSpace}");
                    lines.Add("{");
                    lines.Add($"    [ModelMetadataType(typeof(z_meta{tableName}))]");
                    lines.Add($"    public partial class {tableName}");
                    lines.Add("    {");
                    lines.Add("    }");
                    lines.Add("}");
                    lines.Add("");
                    lines.Add($"public class z_meta{tableName}");
                    lines.Add("{");
                    foreach (var col in cols)
                    {
                        colComments = (col.Comments == null) ? col.ColumnName : col.Comments;
                        colType = codeService.GetPropertyType(col);
                        if (col.IsIdentity == "Y")
                        {
                            lines.Add("    [Key]");
                        }
                        else
                        {
                            lines.Add($"    [Display(Name = \"{colComments}\")]");
                            if (colType == "DateTime")
                                lines.Add("    [DisplayFormat(ApplyFormatInEditMode = true , DataFormatString = \"{0:yyyy/MM/dd}\")]");
                        }
                        colNull = "";
                        if (col.IsNullable == "Y") colNull = "?";
                        str_line = $"    public {colType}{colNull}";
                        str_line += $" {col.ColumnName} ";
                        str_line += "{ get; set; } = ";
                        if (colType == "string") str_line += "\"\";";
                        if (colType == "int") str_line += "0;";
                        if (colType == "decimal") str_line += "0;";
                        if (colType == "DateTime") str_line += "DateTime.Today;";
                        if (colType == "bool") str_line += "false;";
                        lines.Add(str_line);
                    }
                    lines.Add("}");
                }
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