using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class CodeGeneratorService : BaseClass
    {
        public List<string> GetAreaList()
        {
            List<string> values = new List<string>();
            values.Add("空白");
            string path = Directory.GetCurrentDirectory();
            string pathAreaName = Path.Combine(path, "Areas");
            if (Directory.Exists(pathAreaName))
            {
                var forderList = Directory.GetDirectories(pathAreaName, "*", SearchOption.TopDirectoryOnly).ToList();
                foreach (var item in forderList)
                {
                    values.Add(new FileInfo(Path.GetFileName(item)).Name);
                }
            }
            values.Sort();
            return values;
        }
        public List<SelectListItem> GetAreaDropDownList()
        {
            return GetAreaList().Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
        }
        public List<string> GetControllerList(string pathName)
        {
            List<string> values = new List<string>();
            string path = Directory.GetCurrentDirectory();
            string pathControllerName = Path.Combine(path, pathName);
            if (Directory.Exists(pathControllerName))
            {
                var forderList = Directory.GetFiles(pathControllerName, "*", SearchOption.TopDirectoryOnly).ToList();
                foreach (var item in forderList)
                {
                    values.Add(new FileInfo(Path.GetFileName(item)).Name);
                }
            }
            values.Sort();
            return values;
        }
        public List<SelectListItem> GetControllerDropDownList()
        {
            string fileName = "";
            string pathName = "Controllers";
            List<string> controllers = new List<string>();
            var values = GetControllerList(pathName);
            foreach (var item1 in values)
            {
                fileName = $"{pathName}/{item1}";
                controllers.Add(fileName);
            }
            var areas = GetAreaList();
            foreach (var item in areas)
            {
                pathName = $"Areas/{item}/Controllers";
                values = GetControllerList(pathName);
                foreach (var item2 in values)
                {
                    fileName = $"{pathName}/{item2}";
                    controllers.Add(fileName);
                }
            }
            return controllers.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
        }
        public List<string> GetLayoutList()
        {
            List<string> values = new List<string>();
            values.Add("_Layout");
            string path = Directory.GetCurrentDirectory();
            string pathAreaName = Path.Combine(path, "Views\\Shared");
            if (Directory.Exists(pathAreaName))
            {
                var fileList = Directory.GetFiles(pathAreaName, "_Layout*.cshtml", SearchOption.TopDirectoryOnly).ToList();
                foreach (var item in fileList)
                {
                    string str_file_name = new FileInfo(Path.GetFileName(item)).Name;
                    if (str_file_name != "_Layout.cshtml")
                    {
                        string str_file_path = $"~/Views/Shared/{str_file_name}";
                        values.Add(str_file_path);
                    }
                }
            }
            return values;
        }
        public List<SelectListItem> GetLayoutDropDownList()
        {
            return GetLayoutList().Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
        }
        public List<string> GetTableModelList()
        {

            string nameSpace = $"{CodeSessionService.ProjectName}.Models";
            List<string> values = new List<string>();
            var handlers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.IsClass &&
                    p.Name != "dbEntities" &&
                    p.Name != "ErrorViewModel" &&
                    p.Name != "<>c")
                .ToList();
            foreach (var item in handlers)
            {
                if (item.Namespace == nameSpace || item.Name.StartsWith("vm") || item.Name.StartsWith("dm"))
                {
                    values.Add(item.FullName);
                }
            }
            values.Sort();
            return values;
        }
        public List<SelectListItem> GetTableModelDropDownList()
        {
            return GetTableModelList().Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
        }
        public List<CodePropertyModel> GetPropertyModelList(string modelName, string className, string metaCodeName)
        {
            List<CodePropertyModel> values = new List<CodePropertyModel>();
            bool bln_hidden = false;
            bool bln_key = false;
            bool bln_required = false;
            bool bln_checkbox = false;
            string str_display = "";
            string str_dropdown = "";
            string str_format = "";
            string str_default = "";
            string str_full_name = modelName;
            string str_meta_name = $"{metaCodeName}{className}";
            if (string.IsNullOrEmpty(metaCodeName)) str_meta_name = str_full_name;
            PropertyInfo[] myPropertyInfo = null;
            PropertyInfo[] myMetadataInfo = null;
            if (Type.GetType(str_full_name) != null) myPropertyInfo = Type.GetType(str_full_name).GetProperties();
            if (Type.GetType(str_meta_name) != null) myMetadataInfo = Type.GetType(str_meta_name).GetProperties();
            if (myPropertyInfo != null)
            {
                foreach (var item in myPropertyInfo)
                {
                    bln_hidden = false;
                    bln_key = false;
                    bln_required = false;
                    bln_checkbox = false;
                    str_display = "";
                    str_format = "";
                    str_dropdown = "";
                    str_default = "";
                    if (myMetadataInfo != null)
                    {
                        PropertyInfo metaName = myMetadataInfo.Where(m => m.Name == item.Name).FirstOrDefault();
                        if (metaName != null)
                        {
                            DisplayAttribute display = (DisplayAttribute)Attribute.GetCustomAttribute(metaName, typeof(DisplayAttribute));
                            KeyAttribute key = (KeyAttribute)Attribute.GetCustomAttribute(metaName, typeof(KeyAttribute));
                            RequiredAttribute required = (RequiredAttribute)Attribute.GetCustomAttribute(metaName, typeof(RequiredAttribute));
                            DisplayFormatAttribute format = (DisplayFormatAttribute)Attribute.GetCustomAttribute(metaName, typeof(DisplayFormatAttribute));
                            ColumnAttribute column = (ColumnAttribute)Attribute.GetCustomAttribute(metaName, typeof(ColumnAttribute));

                            bln_key = (key == null) ? false : true;
                            bln_required = (required == null) ? false : true;
                            str_display = (display == null) ? item.Name : display.Name;
                            str_format = (format == null) ? "" : format.DataFormatString;
                        }
                    }
                    if (item.Name == "Id" && bln_key == false) bln_key = true;
                    var prop = GetCodePropertyModel(item.Name, item.PropertyType.Name, item.PropertyType.FullName);
                    prop.IsHidden = bln_hidden;
                    prop.IsKeyColumn = bln_key;
                    prop.IsRequired = bln_required;
                    prop.DisplayName = str_display;
                    prop.DataFormat = str_format;
                    prop.IsCheckBox = bln_checkbox;
                    prop.DropdownClass = str_dropdown;
                    prop.DefaultValue = str_default;
                    if (prop.PropertyType == "DateOnly") prop.PropertyType = "DateTime";
                    values.Add(prop);
                }
            }
            return values;
        }
        public List<SelectListItem> GetPropertyDropDownList(string modelName, string className, string metaCodeName)
        {
            List<CodePropertyModel> values = GetPropertyModelList(modelName, className, metaCodeName);
            var PropertyList = values
            .Select(x => new SelectListItem()
            {
                Value = x.PropertyName,
                Text = x.PropertyName
            })
            .OrderBy(x => x.Value)
            .ToList();
            return PropertyList;
        }
        public List<SelectListItem> GetPropertyStringDropDownList(string modelName, string className, string metaCodeName)
        {
            List<CodePropertyModel> values = GetPropertyModelList(modelName, className, metaCodeName)
                .Where(m => m.PropertyType == "string").ToList();
            var PropertyList = values
            .Select(x => new SelectListItem()
            {
                Value = x.PropertyName,
                Text = x.PropertyName
            })
            .OrderBy(x => x.Value)
            .ToList();
            return PropertyList;
        }
        public List<string> GetConnNameList()
        {
            using var sql = new CodeSQLServer();
            return sql.GetConnNameList();
        }
        public List<SelectListItem> GetConnNameDropDownList()
        {
            List<SelectListItem> values = new List<SelectListItem>();
            var data = GetConnNameList();
            if (data.Count > 0)
            {
                values = data.Select(x => new SelectListItem() { Value = x, Text = x }).ToList();
            }
            return values;
        }
        public string GetConnectionString(string connName)
        {
            using var sql = new CodeSQLServer();
            var value = sql.GetConnectionString(connName);
            if (string.IsNullOrEmpty(value)) value = "";
            return value;
        }
        public List<CodeTableViewModel> GetTableViewList(string connName, bool createView = true)
        {
            using var dpr = new DapperRepository(connName);
            string str_query = "";
            if (createView)
            {
                str_query = @"
SELECT object_id AS Id , name AS Name , 'Table' AS TypeName FROM sys.tables WHERE name <> 'sysdiagrams' 
UNION ALL 
SELECT object_id AS Id , name AS Name , 'View' AS TypeName FROM sys.views 
ORDER BY TypeName , Name
";
            }
            else
            {
                str_query = "SELECT object_id AS Id , name AS Name , 'Table' AS TypeName FROM sys.tables WHERE name <> 'sysdiagrams' ORDER BY name";
            }
            return dpr.ReadAll<CodeTableViewModel>(str_query);
        }
        public List<SelectListItem> GetTableViewDropDownList(string connName, bool createView = true)
        {
            var tables = GetTableViewList(connName, createView);
            var values = tables.Select(x => new SelectListItem()
            {
                Value = x.Name,
                Text = x.Name
            })
            .OrderBy(m => m.Value)
            .ToList();
            return values;
        }

        public CodePropertyModel GetCodePropertyModel(string propertyName, string typeName, string fullName)
        {
            CodePropertyModel values = new CodePropertyModel();
            values.PropertyName = propertyName;
            if (typeName == "Nullable`1")
            {
                //System.Nullable`1[[System.Boolean, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
                values.AllowNull = "是";
                int int_start = fullName.IndexOf("[[System.") + 9;
                int int_end = fullName.IndexOf(",");
                int int_leng = int_end - int_start;
                values.PropertyType = fullName.Substring(int_start, int_leng);
                values.FullType = "";
                string str_typeName = fullName.Substring(int_start, int_leng);
                if (str_typeName == "Int32") values.FullType = "Nullable<int>";
                if (str_typeName == "Boolean") values.FullType = "Nullable<bool>";
                if (str_typeName == "DateTime") values.FullType = "Nullable<System.DateTime>";
                if (str_typeName == "Decimal") values.FullType = "Nullable<decimal>";
                if (str_typeName == "DateOnly") values.FullType = "Nullable<System.DateTime>";
                if (string.IsNullOrEmpty(values.FullType)) values.FullType = $"Nullable<{str_typeName.ToLower()}>";
            }
            else
            {
                values.AllowNull = "否";
                string str_column_type = "";
                if (typeName == "Int32") str_column_type = "int";
                if (typeName == "Boolean") str_column_type = "bool";
                if (typeName == "DateTime") str_column_type = "DateTime";
                if (typeName == "DateOnly") str_column_type = "DateTime";
                if (string.IsNullOrEmpty(str_column_type)) str_column_type = typeName.ToLower();
                values.PropertyType = str_column_type;
                values.FullType = str_column_type;
            }
            return values;
        }

        public string CheckConnectionString(string connName)
        {
            using var dpr = new DapperRepository();
            string str_message = dpr.CheckConnectionName(connName);
            return str_message;
        }

        public List<CodeTableModel> GetTableModelFromSQLSelect(string sqlSelectSyntax)
        {
            List<CodeTableModel> values = new List<CodeTableModel>();
            var data = sqlSelectSyntax.Replace('\n', ' ').Replace('\r', ' ').Trim();
            if (data.ToUpper().Substring(0, 7) == "SELECT ")
            {
                int int_pos = data.ToUpper().IndexOf(" FROM ");
                if (int_pos > 0)
                {
                    var dataSelect = data.Substring(0, int_pos);
                    data = data.Substring(int_pos + 6, (data.Length - int_pos - 6)).Trim();
                    dataSelect = dataSelect.Substring(7, dataSelect.Length - 8).Trim();
                    var cols = dataSelect.Split(',').ToList();
                    if (cols.Count > 0)
                    {
                        for (int i = 0; i < cols.Count; i++)
                        {
                            cols[i] = cols[i].Trim();
                        }

                        int_pos = data.IndexOf(" ");
                        string columnData = "";
                        string columnName = "";
                        string columnType = "";
                        var tableName = data.Substring(0, int_pos).Trim();
                        foreach (var col in cols)
                        {
                            columnData = col;
                            if (col.IndexOf(".") <= 0) columnData = $"{tableName}.{col}";
                            tableName = columnData.Split('.')[0];
                            columnName = columnData.Split('.')[1];
                            var colData = CodeTableModelColumn(tableName, columnName);
                            if (colData != null)
                            {
                                columnType = GetColumnType(colData.DataType);
                                if (columnType == "string")
                                {
                                    colData.ColumnName = columnData;
                                    colData.DataType = "string";
                                    values.Add(colData);
                                }
                            }
                        }
                    }
                }
            }
            return values;
        }

        public CodeTableModel CodeTableModelColumn(string tableName, string columnName)
        {
            var data = CodeTableModelColumnList(tableName);
            return data.Where(m => m.ColumnName == columnName).FirstOrDefault();
        }

        public List<CodeTableModel> CodeTableModelColumnList(string tableName)
        {
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

            string str_sql = $"{str_query} '{tableName}' ORDER BY col.colid";
            return dpr.ReadAll<CodeTableModel>(str_sql);
        }

        public string GetPropertyType(CodeTableModel model)
        {
            string str_type = model.DataType.ToLower();
            return GetColumnType(str_type, model.IsNullable);
        }

        public string GetColumnType(string colType, string isNullable = "")
        {
            string str_value = "string";
            if (colType == "char" || colType == "varchar" || colType == "nchar" || colType == "nvarchar")
                str_value = "string";
            else if (colType == "date" || colType == "datetime" || colType == "datetime2" || colType == "smalldatetime" || colType == "timestamp")
                str_value = "DateTime";
            else if (colType == "int")
                str_value = "int";
            else if (colType == "bit")
                str_value = "bool";
            else if (colType == "decimal")
                str_value = "decimal";
            if (isNullable == "YES") str_value += "?";
            return str_value;
        }

        public string GetTableType(string tableName)
        {
            string value = "";
            string typeName = "";
            using var drp = new DapperRepository();
            string str_query = $"SELECT type AS Name FROM sys.objects WHERE name = '{tableName}'";
            var data = drp.ReadSingle<CodeTableViewModel>(str_query);
            if (data != null)
            {
                typeName = data.Name.Trim().ToUpper();
                if (typeName == "U") value = "Table";
                if (typeName == "V") value = "View";
            }
            return value;
        }

        public List<string> GetTableHasKey(string tableName)
        {
            List<string> value = new List<string>();
            string str_value = "";
            string str_pk_name = "";
            string str_clustered = "false";
            using var dpr = new DapperRepository();
            string str_query = @"
SELECT DISTINCT t.object_id AS TableId, t.name AS TableName, i.name AS IndexName, STUFF(REPLACE(REPLACE 
((SELECT c.name + CASE WHEN ic.is_descending_key = 1 THEN ' DESC' ELSE '' END AS [data()] 
FROM  sys.index_columns AS ic INNER JOIN 
sys.columns AS c ON ic.object_id = c.object_id AND ic.column_id = c.column_id 
WHERE ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.is_included_column = 0 
ORDER BY   ic.key_ordinal FOR XML PATH), '<row>', ', '), '</row>', ''), 1, 2, '') AS IndexColumn, 
CASE WHEN i.type_desc = 'CLUSTERED' THEN 'Y' ELSE 'N' END AS IsClustered, 
CASE WHEN i.is_primary_key = 1 THEN 'Y' ELSE 'N' END AS IsPrimaryKey, 
CASE WHEN i.is_unique = 1 THEN 'Y' ELSE 'N' END AS IsUnique 
FROM sys.tables AS t 
INNER JOIN sys.indexes AS i ON t .object_id = i.object_id 
LEFT JOIN sys.dm_db_index_usage_stats AS u ON i.object_id = u.object_id AND i.index_id = u.index_id 
WHERE t .is_ms_shipped = 0 AND i.type <> 0 AND i.is_primary_key = 1 AND ";
            str_query += $"t.name = '{tableName}'";
            var data = dpr.ReadSingle<CodeTableModel>(str_query);
            if (data != null)
            {
                str_pk_name = $"PK_{tableName}";
                if (data.IsClustered == "Y")
                    str_clustered = "true";
                else
                    str_clustered = "false";
                str_value = $"                entity.HasKey(e => e.{data.IndexColumn})";
                if (data.IndexName == str_pk_name)
                {
                    str_value += $".IsClustered({str_clustered});";
                    value.Add(str_value);
                }
                else
                {
                    value.Add(str_value);
                    str_value = $"                    .HasName(\"{data.IndexName}\")";
                    value.Add(str_value);
                    str_value = $"                    .IsClustered({str_clustered});";
                    value.Add(str_value);
                }
            }
            return value;
        }

        public List<string> GetTableHasIndex(string tableName)
        {
            List<string> value = new List<string>();
            string str_value = "";
            string str_index_data = "";
            string str_index_name = "";
            string str_dscending = "";
            using var dpr = new DapperRepository();
            string str_query = @"
SELECT Distinct 
	t.object_id AS TableId ,
    t.name AS TableName,
    i.name AS IndexName,
    STUFF(REPLACE(REPLACE((
        SELECT c.name + CASE WHEN ic.is_descending_key = 1 THEN ' DESC' ELSE '' END AS [data()] 
        FROM sys.index_columns AS ic 
        INNER JOIN sys.columns AS c ON ic.object_id = c.object_id AND ic.column_id = c.column_id 
        WHERE ic.object_id = i.object_id AND ic.index_id = i.index_id AND ic.is_included_column = 0 
        ORDER BY ic.key_ordinal 
        FOR XML PATH 
    ), '<row>', ', '), '</row>', ''), 1, 2, '') AS IndexColumn ,
	CASE WHEN i.type_desc = 'CLUSTERED' THEN 'Y' ELSE 'N' END AS IsClustered , 
	CASE WHEN i.is_primary_key = 1 THEN 'Y' ELSE 'N' END AS IsPrimaryKey, 
    CASE WHEN i.is_unique = 1 THEN 'Y' ELSE 'N' END AS IsUnique 
FROM sys.tables AS t 
INNER JOIN sys.indexes AS i ON t.object_id = i.object_id 
LEFT JOIN sys.dm_db_index_usage_stats AS u ON i.object_id = u.object_id AND i.index_id = u.index_id 
WHERE t.is_ms_shipped = 0 AND i.type <> 0 AND i.is_primary_key = 0 AND t.name = ";
            str_query += $"'{tableName}'";
            var data = dpr.ReadAll<CodeTableModel>(str_query);
            if (data != null)
            {
                foreach (var item in data)
                {
                    str_index_name = item.IndexName;
                    str_dscending = "";
                    str_value = $"                entity.HasIndex(e => ";
                    if (item.IndexColumn.IndexOf(',') > 0)
                    {
                        str_value += "new {";
                        var cols = item.IndexColumn.Split(',').ToList();
                        for (int i = 0; i < cols.Count; i++)
                        {
                            str_value += "e.";
                            str_index_data = cols[i].Trim();
                            if (cols[i].IndexOf(" DESC") > 0)
                            {
                                str_dscending = "true";
                                str_value += str_index_data.Split(" ")[0];
                            }
                            else
                            {
                                str_value += str_index_data;
                            }
                            if ((i + 1) < cols.Count) str_value += ",";
                        }
                        str_value += "}";
                    }
                    else
                    {
                        str_index_data = item.IndexColumn.Trim();
                        if (str_index_data.IndexOf(" DESC") > 0)
                        {
                            str_dscending = "true";
                            str_index_data = str_index_data.Split(" ")[0];
                        }
                        str_value += "e.";
                        str_value += str_index_data;
                    }
                    str_value += $",\"{str_index_name}\")";
                    if (!string.IsNullOrEmpty(str_dscending))
                    {
                        value.Add(str_value);
                        str_value = "                    .IsDescending(";
                        var cols = item.IndexColumn.Split(',').ToList();
                        for (int i = 0; i < cols.Count; i++)
                        {
                            str_index_data = cols[i].Trim();
                            if (cols[i].IndexOf(" DESC") > 0)
                                str_value += "true";
                            else
                                str_value += "false";
                            if ((i + 1) < cols.Count) str_value += ",";
                        }
                        str_value += ")";
                        if (item.IsClustered != "Y") str_value += ";";
                        value.Add(str_value);
                        str_value = "";
                    }

                    if (item.IsClustered == "Y")
                    {
                        if (string.IsNullOrEmpty(str_value)) str_value += "                    ";
                        str_value += ".IsClustered();";
                    }
                    else if (string.IsNullOrEmpty(str_dscending))
                        str_value += ";";
                    value.Add(str_value);
                    value.Add("");
                }
            }
            return value;
        }

        public List<SelectListItem> GetSortColumnList(CodeGeneratorModel model)
        {
            var values = new List<SelectListItem>();
            using var codeService = new CodeGeneratorService();
            using var dpr = new DapperRepository();
            if (!model.SelectAll && (model.TableSelectType == "1"))
            {
                if (model.UseSQLSelect)
                {
                    if (model.UseSQLSelect && !string.IsNullOrEmpty(model.SQLSelectSyntax))
                    {
                        values = codeService.GetTableModelFromSQLSelect(model.SQLSelectSyntax)
                            .Select(m => new SelectListItem()
                            {
                                Value = m.ColumnName,
                                Text = m.ColumnName
                            }).ToList();
                    }
                }
                else
                {
                    var entity = $"{AppService.ProjectName}.Models.{model.TableViewName}";
                    values = dpr.GetSQLSelectCommandList(entity, 0)
                    .Select(m => new SelectListItem()
                    {
                        Value = m,
                        Text = m
                    }).ToList();
                }
            }
            return values;

        }
    }
}