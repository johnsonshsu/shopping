using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public string CreateView(CodeGeneratorModel model)
        {
            CodeSessionService.Affecteds++;
            string str_message = "";
            string path = Directory.GetCurrentDirectory();
            string pathViewName = "";
            string fileName = $"{model.ViewName}.cshtml";

            if (!string.IsNullOrEmpty(model.AreaName))
            {
                path += $"\\Areas\\{model.AreaName}";
            }

            // var data = model.ControllerName.Split('/').ToList();
            // model.AreaName = "";
            // int int_index = 0;
            // if (data.Count == 2) int_index = 1;
            // if (data.Count == 4) int_index = 3;
            // if (int_index == 3) model.AreaName = data[1];
            // model.ControllerName = data[int_index];
            // if (model.ControllerName.Length > 13)
            // {
            //     model.ControllerName = model.ControllerName.Substring(0, data[1].Length - 13);
            // }
            // if (!string.IsNullOrEmpty(model.AreaName))
            // {
            //     path += $"\\Areas\\{model.AreaName}";
            // }

            path += $"\\Views";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path += $"\\{model.ControllerName}";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            pathViewName = Path.Combine(path, fileName);
            if (File.Exists(pathViewName))
            {
                using var msg = new CodeMessage();
                str_message = msg.CreateViewMessage(model, true);
                if (!model.ForceOverride)
                {
                    CodeSessionService.Affecteds--;
                    return str_message;
                }
                File.Delete(pathViewName);
            }
            List<string> lines = new List<string>();
            try
            {
                if (model.TemplateName == "List") lines = CreateViewList(model);
                if (model.TemplateName == "Empty") lines = CreateViewEmpty(model);
                if (model.TemplateName == "Edit" && model.LayoutColCount == 1) lines = CreateViewEdit1(model);
                if (model.TemplateName == "Edit" && model.LayoutColCount != 1) lines = CreateViewEdit2(model);
                if (model.TemplateName == "Create" && model.LayoutColCount == 1) lines = CreateViewCreate1(model);
                if (model.TemplateName == "Create" && model.LayoutColCount != 1) lines = CreateViewCreate2(model);
                if (model.TemplateName == "Delete" && model.LayoutColCount == 1) lines = CreateViewDelete1(model);
                if (model.TemplateName == "Delete" && model.LayoutColCount != 1) lines = CreateViewDelete2(model);
                if (model.TemplateName == "Details" && model.LayoutColCount == 1) lines = CreateViewDetails1(model);
                if (model.TemplateName == "Details" && model.LayoutColCount != 1) lines = CreateViewDetails2(model);
                if (model.TemplateName == "CreateEdit" && model.LayoutColCount == 1) lines = CreateViewCreateEdit1(model);
                if (model.TemplateName == "CreateEdit" && model.LayoutColCount != 1) lines = CreateViewCreateEdit2(model);

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