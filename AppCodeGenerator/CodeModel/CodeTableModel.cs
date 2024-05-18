using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class CodeTableModel
    {
        public int TableId { get; set; }
        public string SchemaName { get; set; }
        public string TableType { get; set; }
        public string TableName { get; set; }
        public string IndexName { get; set; }
        public string IndexColumn { get; set; }
        public string IndexType { get; set; }
        public string ColumnId { get; set; }
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public string DataTypeName { get; set; }
        public string DataTypePrec { get; set; }
        public string DataTypeScale { get; set; }
        public string DataTypeLength { get; set; }
        public string DefaultValue { get; set; }
        public string IsNullable { get; set; }
        public string IsIdentity { get; set; }
        public string IsPrimaryKey { get; set; }
        public string IsUnique { get; set; }
        public string IsClustered { get; set; }
        public string Comments { get; set; }
    }
}