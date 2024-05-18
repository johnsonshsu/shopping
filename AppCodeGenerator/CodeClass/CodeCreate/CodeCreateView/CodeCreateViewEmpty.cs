using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public List<string> CreateViewEmpty(CodeGeneratorModel model)
        {
            List<string> lines = new List<string>();
            lines.Add("@{");
            lines.Add($"    ViewData[\"Title\"] = \"" + model.ViewName + "\";");
            if (!string.IsNullOrEmpty(model.LayoutName))
                lines.Add($"    Layout = \"" + model.LayoutName + "\";");
            lines.Add("}");
            lines.Add("<h1>@ViewData[\"Title\"]</h1>");
            return lines;
        }
    }
}