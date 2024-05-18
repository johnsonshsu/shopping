using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public void CreateControllerHeader(ref List<string> lineDatas, CodeGeneratorModel model)
        {
            string str_base = model.BaseController ? "BaseController" : "Controller";
            lineDatas.Add("using Microsoft.AspNetCore.Mvc;");
            if (model.TemplateName == "Form")
                lineDatas.Add("using X.PagedList;");
            lineDatas.Add("");
            lineDatas.Add($"namespace {model.ModelNameSpace}");
            lineDatas.Add("{");
            lineDatas.Add($"    public class {model.ControllerName}Controller : {str_base}");
            lineDatas.Add("    {");
        }
        public void CreateControllerFooter(ref List<string> lineDatas, CodeGeneratorModel model)
        {
            string str_base = model.BaseController ? "BaseController" : "Controller";
            lineDatas.Add("    }");
            lineDatas.Add("}");
        }
        public void CreateActionHeader(ref List<string> lineDatas, string lineSpace, string httpMode, string roleList, CodeGeneratorModel model)
        {
            if (!string.IsNullOrEmpty(model.AreaName)) lineDatas.Add(GetLineValue(lineSpace, $"[Area(\"{model.AreaName}\")]"));
            if (string.IsNullOrEmpty(roleList))
                lineDatas.Add(GetLineValue(lineSpace, "[Login()]"));
            else
                lineDatas.Add(GetLineValue(lineSpace, "[Login(RoleList = \"" + roleList + "\")]"));
            lineDatas.Add(GetLineValue(lineSpace, $"[Http{httpMode}]"));
        }
        public void CreateActionSummary(ref List<string> lineDatas, string lineSpace, string summaryText, CodeGeneratorModel model)
        {
            lineDatas.Add(GetLineValue(lineSpace, "/// <summary>"));
            lineDatas.Add(GetLineValue(lineSpace, $"/// {summaryText}"));
            lineDatas.Add(GetLineValue(lineSpace, "/// </summary>"));
        }
        public void AddViewHeader(ref List<string> lineDatas, CodeGeneratorModel model)
        {
            if (model.UseModel)
            {
                if (model.TemplateName == "List")
                {
                    lineDatas.Add(GetLineValue("@model IEnumerable<" + model.ModelName + ">"));
                }
                else
                {
                    lineDatas.Add(GetLineValue("@model " + model.ModelName));
                }
            }
            lineDatas.Add(GetLineValue("@{"));
            lineDatas.Add(GetLineValue("    ViewData[\"Title\"] = \"" + model.ViewName + "\";"));
            if (!string.IsNullOrEmpty(model.LayoutName))
                lineDatas.Add(GetLineValue("    Layout = \"" + model.LayoutName + "\";"));
            else
                lineDatas.Add(GetLineValue("    Layout = \"_Layout\";"));
            if (model.DropDownColumns != null && model.DropDownColumns.Count() > 0)
            {
                foreach (var item in model.DropDownColumns)
                {
                    lineDatas.Add(GetLineValue($"    var {item}List = new List<SelectListItem>();"));
                }
                foreach (var item in model.DropDownColumns)
                {
                    lineDatas.Add(GetLineValue($"    //using var repo{item} = new z_repoTableName();"));
                }
                foreach (var item in model.DropDownColumns)
                {
                    lineDatas.Add(GetLineValue($"    //{item}List = repo{item}.GetDropDownList(true);"));
                }
            }
            lineDatas.Add(GetLineValue("}"));
            lineDatas.Add(GetLineValue(""));
        }
        public void AddFormHeader(ref List<string> lineDatas, string lineSpace, CodeGeneratorModel model)
        {
            lineDatas.Add(GetLineValue(lineSpace, "@using (Html.BeginForm())"));
            lineDatas.Add(GetLineValue(lineSpace, "{"));
            lineDatas.Add(GetLineValue(lineSpace, "    <div asp-validation-summary=\"ModelOnly\" class=\"text-danger\"></div>"));
            if (!string.IsNullOrEmpty(model.KeyName))
                lineDatas.Add(GetLineValue(lineSpace, "    @Html.HiddenFor(model => model." + model.KeyName + ")"));
            lineDatas.Add(GetLineValue(""));
        }
        public void AddFormFooter(ref List<string> lineDatas, string lineSpace)
        {
            lineDatas.Add(GetLineValue(lineSpace, "    <hr>"));
            lineDatas.Add(GetLineValue(lineSpace, "    <div class=\"row form-group\">"));
            lineDatas.Add(GetLineValue(lineSpace, "        <div class=\"col-md-12\">"));
            lineDatas.Add(GetLineValue(lineSpace, "            @Html.InputButton(enFormInputButton.Submit)"));
            lineDatas.Add(GetLineValue(lineSpace, "            @Html.ActionReturnButton()"));
            lineDatas.Add(GetLineValue(lineSpace, "        </div>"));
            lineDatas.Add(GetLineValue(lineSpace, "    </div>"));
            lineDatas.Add(GetLineValue(lineSpace, "}"));
        }
        public void AddCardHeader(ref List<string> lineDatas, CodeGeneratorModel model)
        {
            lineDatas.Add(GetLineValue("<div class=\"card card-size-" + model.CardSize.ToLower() + "\">"));
            lineDatas.Add(GetLineValue("    <div class=\"card-header bg-primary text-white\">"));
            lineDatas.Add(GetLineValue("        <div class=\"row\">"));
            lineDatas.Add(GetLineValue("            <div class=\"col-md-12\">"));
            lineDatas.Add(GetLineValue("                <div class=\"float-start\">"));
            lineDatas.Add(GetLineValue("                    <h4>@SessionService.ActionName</h4>"));
            lineDatas.Add(GetLineValue("                </div>"));
            lineDatas.Add(GetLineValue("                @if (!string.IsNullOrEmpty(SessionService.SubHeaderName))"));
            lineDatas.Add(GetLineValue("                {"));
            lineDatas.Add(GetLineValue("                    <div class=\"float-end\">"));
            lineDatas.Add(GetLineValue("                        <h4>@SessionService.SubHeaderName</h4>"));
            lineDatas.Add(GetLineValue("                    </div>"));
            lineDatas.Add(GetLineValue("                }"));
            lineDatas.Add(GetLineValue("            </div>"));
            lineDatas.Add(GetLineValue("        </div>"));
            lineDatas.Add(GetLineValue("    </div>"));
            lineDatas.Add(GetLineValue("    <div class=\"card-body\">"));
        }
        public void AddCardFooter(ref List<string> lineDatas, CodeGeneratorModel model)
        {
            lineDatas.Add(GetLineValue("    </div>"));
            lineDatas.Add(GetLineValue("</div>"));
        }
        public string GetLineValue()
        {
            return GetLineValue("", "");
        }
        public string GetLineValue(string lineData)
        {
            return GetLineValue("", lineData);
        }
        public string GetLineValue(string lineSpace, string lineData)
        {
            return $"{lineSpace}{lineData}";
        }
        public void CreateFile(List<string> lineData, string filePath, string fileName)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(filePath, fileName)))
            {
                foreach (string line in lineData)
                    outputFile.WriteLine(line);
            }
        }
    }
}