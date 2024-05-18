using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public string CreateDbContextFile(CodeGeneratorModel model)
        {
            int TableId = 0;
            string str_message = "";
            string path = Directory.GetCurrentDirectory();
            string pathDbContextName = "";
            string fileName = "";
            string tableFileName = "";
            string tableName = "";
            string tableType = "";
            string keyName = "";

            string nameSpace = model.ProjectName;
            fileName += $"{model.DbContextName}.cs";
            path += $"\\Models";
            nameSpace += ".Models";
            model.ModelNameSpace = nameSpace;
            pathDbContextName = Path.Combine(path, fileName);
            if (File.Exists(pathDbContextName))
            {
                if (!model.ForceOverride) return str_message;
                File.Delete(pathDbContextName);
            }
            List<string> lines = new List<string>();
            lines.Add("using Microsoft.EntityFrameworkCore;");
            lines.Add("using System;");
            lines.Add("using System.Collections.Generic;");
            lines.Add("");
            lines.Add($"namespace {nameSpace}");
            lines.Add("{");
            lines.Add($"    public partial class {model.DbContextName} : DbContext");
            lines.Add("    {");
            lines.Add($"        public {model.DbContextName}()");
            lines.Add("        {");
            lines.Add("        }");
            lines.Add("");
            lines.Add($"        public {model.DbContextName}(DbContextOptions<{model.DbContextName}> options)");
            lines.Add("            : base(options)");
            lines.Add("        {");
            lines.Add("        }");
            lines.Add("");

            string pathTables = Path.Combine(path, "Tables");
            var files = Directory.GetFiles(pathTables, "*.cs", SearchOption.TopDirectoryOnly);
            foreach (var file in files)
            {
                tableFileName = Path.GetFileName(file);
                tableName = tableFileName.Substring(0, tableFileName.Length - 3);
                lines.Add("        public virtual DbSet<" + tableName + "> " + tableName + " { get; set; }");
                lines.Add("");
            }

            lines.Add("");
            lines.Add("        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)");
            lines.Add($"            => optionsBuilder.UseSqlServer(\"Name=ConnectionStrings:{model.ConnStringName}\");");
            lines.Add("");
            lines.Add("        protected override void OnModelCreating(ModelBuilder modelBuilder)");
            lines.Add("        {");

            using var codeService = new CodeGeneratorService();
            using var dpr = new DapperRepository();
            string str_query = @"
SELECT OBJECT_SCHEMA_NAME(tab.id,DB_ID()) AS SchemaName, 
tab.id AS TableId, TRIM(tab.xtype) AS TableType , tab.name AS TableName, col.colid AS ColumnId, col.name AS ColumnName, 
CASE WHEN CHARINDEX('char', typ.name) > 0 
THEN CASE col.prec WHEN -1 THEN typ.name + '(max)' ELSE typ.name + '(' + CAST(col.prec AS varchar) + ')' END 
WHEN typ.name = 'decimal' THEN 'decimal(' + CAST(col.prec AS varchar) + ',' + CAST(col.scale AS varchar) + ')' 
ELSE CAST(typ.name AS varchar) END AS DataTypeName, 
typ.name AS DataType, col.prec AS DataTypePrec, col.scale AS DataTypeScale, col.length AS DataTypeLength, com.text AS DefaultValue, 
CASE WHEN col.isnullable = 1 THEN 'Y' ELSE 'N' END AS IsNullable, 
CASE WHEN col.status = 128 THEN 'Y' ELSE 'N' END AS IsIdentity, 
(SELECT value FROM sys.extended_properties WHERE (major_id = OBJECT_ID(tab.name)) AND (minor_id = col.colid)) AS Comments 
FROM sys.sysobjects AS tab 
INNER JOIN sys.syscolumns AS col 
LEFT OUTER JOIN sys.syscomments AS com 
INNER JOIN sys.sysobjects AS obj ON com.id = obj.id ON col.cdefault = com.id AND com.colid = 1 ON tab.id = col.id 
INNER JOIN sys.systypes AS typ ON col.xusertype = typ.xusertype 
WHERE tab.name = ";

            foreach (var file in files)
            {
                tableFileName = Path.GetFileName(file);
                tableName = tableFileName.Substring(0, tableFileName.Length - 3);
                lines.Add($"            modelBuilder.Entity<{tableName}>(entity =>");
                lines.Add("            {");

                string str_sql = $"{str_query} '{tableName}' ORDER BY col.colid";
                var cols = dpr.ReadAll<CodeTableModel>(str_sql);
                if (cols != null && cols.Count() > 0)
                {
                    TableId = 0;
                    var data = cols.FirstOrDefault();
                    if (data != null) TableId = data.TableId;
                    tableType = cols.First().TableType;
                    if (tableType == "V")
                    {
                        lines.Add($"                entity.HasNoKey().ToView(\"{tableName}\");");
                    }
                    if (tableType == "U")
                    {
                        keyName = "";
                        data = cols.Where(m => m.IsIdentity == "Y").FirstOrDefault();
                        if (data != null) keyName = data.ColumnName;
                        var keyData = codeService.GetTableHasKey(tableName);

                        if (keyData.Count() > 0)
                        {
                            foreach (var item in keyData)
                            {
                                lines.Add(item);
                            }
                            lines.Add("");
                        }

                        var ind = codeService.GetTableHasIndex(tableName);
                        foreach (var item in ind)
                        {
                            lines.Add(item);
                        }
                    }
                    foreach (var col in cols)
                    {
                        if (codeService.GetPropertyType(col) == "string")
                            lines.Add($"                entity.Property(e => e.{col.ColumnName}).HasMaxLength({col.DataTypePrec});");
                        if (codeService.GetPropertyType(col) == "DateTime")
                            lines.Add($"                entity.Property(e => e.{col.ColumnName}).HasColumnType(\"datetime\");");
                        if (codeService.GetPropertyType(col) == "decimal")
                            lines.Add($"                entity.Property(e => e.{col.ColumnName}).HasColumnType(\"decimal({col.DataTypePrec}, {col.DataTypeScale})\");");

                    }
                    lines.Add("            });");
                    lines.Add("");
                }
            }
            lines.Add("");
            lines.Add("            OnModelCreatingPartial(modelBuilder);");
            lines.Add("        }");
            lines.Add("        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);");
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
            return str_message;
        }

    }
}