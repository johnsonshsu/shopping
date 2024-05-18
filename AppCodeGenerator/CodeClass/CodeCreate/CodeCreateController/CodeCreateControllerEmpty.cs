using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public List<string> CreateControllerEmpty(CodeGeneratorModel model)
        {
            List<string> lines = new List<string>();
            CreateControllerHeader(ref lines, model);
            if (!string.IsNullOrEmpty(model.AreaName)) lines.Add($"        [Area(\"{model.AreaName}\")]");
            lines.Add("        [HttpGet]");
            lines.Add("        public ActionResult Index()");
            lines.Add("        {");
            lines.Add("            return View();");
            lines.Add("        }");
            CreateControllerFooter(ref lines, model);
            return lines;
        }

    }
}