using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public List<string> CreateViewCreate1(CodeGeneratorModel model)
        {
            using var codeService = new CodeGeneratorService();
            var PropertyList = codeService.GetPropertyModelList(model.ModelName, model.ModelClassName, "z_meta");
            var data = PropertyList.Where(m => m.IsKeyColumn).FirstOrDefault();
            model.KeyName = (data == null) ? "" : data.PropertyName;
            string lineEmpty = "";
            string lineSpace = model.UseCard ? "        " : lineEmpty;
            List<string> lines = new List<string>();
            AddViewHeader(ref lines, model);
            if (model.UseCard) AddCardHeader(ref lines, model);
            AddFormHeader(ref lines, lineSpace, model);
            foreach (var item in PropertyList)
            {
                if (!item.IsKeyColumn)
                {
                    lines.Add(GetLineValue(lineSpace, "    <div class=\"row form-group\">"));
                    lines.Add(GetLineValue(lineSpace, "        <div class=\"control-label col-md-2\">"));
                    lines.Add(GetLineValue(lineSpace, "            @Html.DisplayNameFor(model => model." + item.PropertyName + ")"));
                    lines.Add(GetLineValue(lineSpace, "        </div>"));
                    lines.Add(GetLineValue(lineSpace, "        <div class=\"col-md-10\">"));
                    if (model.DropDownColumns != null && model.DropDownColumns.Where(m => m == item.PropertyName).Count() > 0)
                    {
                        lines.Add(GetLineValue(lineSpace, "                    @Html.DropDownListFor(model => model." + item.PropertyName + ", " + item.PropertyName + "List , new { @class = \"form-control selectpicker\" , data_live_search = \"true\" })"));
                    }
                    else if (item.PropertyType == "bool")
                        lines.Add(GetLineValue(lineSpace, "                    @Html.CheckBoxFor(model => model." + item.PropertyName + ", new { htmlAttributes = new { @class = \"form-control\" } })"));
                    else if (item.PropertyType == "DateTime" || item.PropertyType == "DateOnly")
                        lines.Add(GetLineValue(lineSpace, "                    @Html.EditorFor(model => model." + item.PropertyName + ", new { htmlAttributes = new { @class = \"form-control datepicker\" , type=\"text\" } })"));
                    else if (item.PropertyType == "int" || item.PropertyType == "decimal")
                        lines.Add(GetLineValue(lineSpace, "                    @Html.EditorFor(model => model." + item.PropertyName + ", new { htmlAttributes = new { @class = \"form-control\" ,type=\"number\" } })"));
                    else
                        lines.Add(GetLineValue(lineSpace, "                    @Html.EditorFor(model => model." + item.PropertyName + ", new { htmlAttributes = new { @class = \"form-control\" } })"));
                    lines.Add(GetLineValue(lineSpace, "            @Html.ValidationMessageFor(model => model." + item.PropertyName + ", \"\", new { @class = \"text-danger\" })"));
                    lines.Add(GetLineValue(lineSpace, "        </div>"));
                    lines.Add(GetLineValue(lineSpace, "    </div>"));
                }
            }
            AddFormFooter(ref lines, lineSpace);
            if (model.UseCard) AddCardFooter(ref lines, model);
            return lines;
        }
        public List<string> CreateViewCreate2(CodeGeneratorModel model)
        {
            using var codeService = new CodeGeneratorService();
            var PropertyList = codeService.GetPropertyModelList(model.ModelName, model.ModelClassName, "z_meta");
            var data = PropertyList.Where(m => m.IsKeyColumn).FirstOrDefault();
            model.KeyName = (data == null) ? "" : data.PropertyName;
            int int_count = 0;
            int int_cols = 0;
            string lineEmpty = "";
            string lineSpace = model.UseCard ? "        " : lineEmpty;
            string sectionSize = (12 / model.LayoutColCount).ToString();
            string labelSize = model.LabelScaleSize.ToString();
            string columnSize = (12 - model.LabelScaleSize).ToString();
            List<string> lines = new List<string>();
            AddViewHeader(ref lines, model);
            if (model.UseCard) AddCardHeader(ref lines, model);
            AddFormHeader(ref lines, lineSpace, model);
            foreach (var item in PropertyList)
            {
                if (!item.IsKeyColumn)
                {
                    if (int_count == 0 || (int_count % model.LayoutColCount) == 0)
                    {
                        lines.Add(GetLineValue(lineSpace, "    <div class=\"row\">"));
                    }
                    int_count++;
                    if (int_count == 1)
                    {
                        int_cols = 1;
                    }
                    else
                    {
                        int_cols++;
                        if (int_cols > model.LayoutColCount) int_cols = 1;
                    }
                    lines.Add(GetLineValue(lineSpace, $"        <div class=\"col-md-{sectionSize}\">"));
                    lines.Add(GetLineValue(lineSpace, "            <div class=\"row form-group\">"));
                    lines.Add(GetLineValue(lineSpace, $"                <div class=\"control-label col-md-{labelSize}\">"));
                    lines.Add(GetLineValue(lineSpace, "                    @Html.DisplayNameFor(model => model." + item.PropertyName + ")"));
                    lines.Add(GetLineValue(lineSpace, "                </div>"));
                    lines.Add(GetLineValue(lineSpace, $"                <div class=\"col-md-{columnSize}\">"));
                    if (model.DropDownColumns != null && model.DropDownColumns.Where(m => m == item.PropertyName).Count() > 0)
                    {
                        lines.Add(GetLineValue(lineSpace, "                    @Html.DropDownListFor(model => model." + item.PropertyName + ", " + item.PropertyName + "List , new { @class = \"form-control selectpicker\" , data_live_search = \"true\" })"));
                    }
                    else if (item.PropertyType == "bool")
                        lines.Add(GetLineValue(lineSpace, "                    @Html.CheckBoxFor(model => model." + item.PropertyName + ", new { htmlAttributes = new { @class = \"form-control\" } })"));
                    else if (item.PropertyType == "DateTime" || item.PropertyType == "DateOnly")
                        lines.Add(GetLineValue(lineSpace, "                    @Html.EditorFor(model => model." + item.PropertyName + ", new { htmlAttributes = new { @class = \"form-control datepicker\" , type=\"text\" } })"));
                    else if (item.PropertyType == "int" || item.PropertyType == "decimal")
                        lines.Add(GetLineValue(lineSpace, "                    @Html.EditorFor(model => model." + item.PropertyName + ", new { htmlAttributes = new { @class = \"form-control\" ,type=\"number\" } })"));
                    else
                        lines.Add(GetLineValue(lineSpace, "                    @Html.EditorFor(model => model." + item.PropertyName + ", new { htmlAttributes = new { @class = \"form-control\" } })"));
                    lines.Add(GetLineValue(lineSpace, "                    @Html.ValidationMessageFor(model => model." + item.PropertyName + ", \"\", new { @class = \"text-danger\" })"));
                    lines.Add(GetLineValue(lineSpace, "                </div>"));
                    lines.Add(GetLineValue(lineSpace, "            </div>"));
                    lines.Add(GetLineValue(lineSpace, "        </div>"));
                    if ((int_count % model.LayoutColCount) == 0)
                    {
                        lines.Add(GetLineValue(lineSpace, "    </div>"));
                    }
                }
            }
            if (int_cols < model.LayoutColCount)
            {
                for (int i = 1; i <= (model.LayoutColCount - int_cols); i++)
                {
                    lines.Add(GetLineValue(lineSpace, $"        <div class=\"col-md-{sectionSize}\">"));
                    lines.Add(GetLineValue(lineSpace, "            <div class=\"row form-group\">"));
                    lines.Add(GetLineValue(lineSpace, $"                <div class=\"control-label col-md-{labelSize}\">"));
                    lines.Add(GetLineValue(lineSpace, "                </div>"));
                    lines.Add(GetLineValue(lineSpace, $"                <div class=\"col-md-{columnSize}\">"));
                    lines.Add(GetLineValue(lineSpace, "                </div>"));
                    lines.Add(GetLineValue(lineSpace, "            </div>"));
                    lines.Add(GetLineValue(lineSpace, "        </div>"));
                }
                lines.Add(GetLineValue(lineSpace, "    </div>"));
            }

            AddFormFooter(ref lines, lineSpace);
            if (model.UseCard) AddCardFooter(ref lines, model);
            return lines;
        }
    }
}