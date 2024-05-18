using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public string CreateDbContext(CodeGeneratorModel model)
        {
            string str_message = "";
            if (model.DBContextType != "DBContextOnly") CreateTablesFile(model);
            if (model.DBContextType != "TableOnly") CreateDbContextFile(model);
            return str_message;
        }
    }
}