using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public string CreateController(CodeGeneratorModel model)
        {
            CodeSessionService.Affecteds++;
            string str_message = "";
            string path = Directory.GetCurrentDirectory();
            string pathControllerName = "";
            string fileName = $"{model.ControllerName}Controller.cs";
            string nameSpace = model.ProjectName;
            if (!string.IsNullOrEmpty(model.AreaName))
            {
                path += $"\\Areas\\{model.AreaName}";
                nameSpace += $".Areas.{model.AreaName}";
            }
            path += $"\\Controllers";
            nameSpace += ".Controllers";
            model.ModelNameSpace = nameSpace;
            pathControllerName = Path.Combine(path, fileName);
            if (File.Exists(pathControllerName))
            {
                using var msg = new CodeMessage();
                str_message = msg.CreateControllerMessage(model, true);
                if (!model.ForceOverride)
                {
                    CodeSessionService.Affecteds--;
                    return str_message;
                }

                File.Delete(pathControllerName);
            }
            try
            {
                List<string> lines = new List<string>();
                if (model.TemplateName == "Empty") lines = CreateControllerEmpty(model);
                if (model.TemplateName == "Form") lines = CreateControllerForm(model);
                CreateFile(lines, path, fileName);

            }
            catch (Exception ex)
            {
                CodeSessionService.Affecteds--;
                str_message = ex.Message;
            }
            return str_message;
        }
    }
}