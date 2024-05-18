using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public List<string> CreateViewDelete1(CodeGeneratorModel model)
        {
            using var codeService = new CodeGeneratorService();
            var PropertyList = codeService.GetPropertyModelList(model.ModelName, model.ModelClassName, "z_meta");
            var data = PropertyList.Where(m => m.IsKeyColumn).FirstOrDefault();
            model.KeyName = (data == null) ? "" : data.PropertyName;
            string lineEmpty = "";
            string lineSpace = model.UseCard ? "        " : lineEmpty;
            string labelSize = model.LabelScaleSize.ToString();
            string columnSize = (12 - model.LabelScaleSize).ToString();
            List<string> lines = new List<string>();
            AddViewHeader(ref lines, model);
            if (model.UseCard) AddCardHeader(ref lines, model);
            lines.Add(GetLineValue(lineSpace, "<h3>您確定要刪除此筆資料?</h3>"));
            lines.Add(GetLineValue(lineSpace, "<div>"));
            lines.Add(GetLineValue(lineSpace, "    <dl class=\"row\">"));
            foreach (var item in PropertyList)
            {
                if (item.PropertyName != model.KeyName)
                {
                    if (!item.IsKeyColumn)
                    {
                        lines.Add(GetLineValue(lineSpace, $"        <dt class = \"col-md-{labelSize}\">"));
                        lines.Add(GetLineValue(lineSpace, "            @Html.DisplayNameFor(model => model." + item.PropertyName + ")"));
                        lines.Add(GetLineValue(lineSpace, "        </dt>"));
                        lines.Add(GetLineValue(lineSpace, $"        <dt class = \"col-md-{columnSize}\">"));
                        lines.Add(GetLineValue(lineSpace, "            @Html.DisplayFor(model => model." + item.PropertyName + ")"));
                        lines.Add(GetLineValue(lineSpace, "        </dt>"));
                    }
                }
            }
            lines.Add(GetLineValue());
            lines.Add(GetLineValue(lineSpace, "@{"));
            lines.Add(GetLineValue(lineSpace, $"        ActionService.RowId = Model.{model.KeyName};"));
            lines.Add(GetLineValue(lineSpace, "}"));
            lines.Add(GetLineValue(lineSpace, "    @using (Html.BeginForm())"));
            lines.Add(GetLineValue(lineSpace, "    {"));
            lines.Add(GetLineValue(lineSpace, $"        @Html.HiddenFor(model => model.{model.KeyName})"));
            lines.Add(GetLineValue(lineSpace, "        @await Html.PartialAsync(\"_PartialFormDelete\")"));
            lines.Add(GetLineValue(lineSpace, "    }"));
            lines.Add(GetLineValue(lineSpace, "</div>"));
            if (model.UseCard) AddCardFooter(ref lines, model);
            return lines;
        }

        public List<string> CreateViewDelete2(CodeGeneratorModel model)
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
            lines.Add(GetLineValue(lineSpace, "<h3>您確定要刪除此筆資料?</h3>"));
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
                    lines.Add(GetLineValue(lineSpace, $"                <div class=\"control-label col-md-{columnSize}\">"));
                    lines.Add(GetLineValue(lineSpace, "                    @Html.DisplayFor(model => model." + item.PropertyName + ")"));
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
                    lines.Add(GetLineValue(lineSpace, $"                <div class=\"control-label col-md-{columnSize}\">"));
                    lines.Add(GetLineValue(lineSpace, "                </div>"));
                    lines.Add(GetLineValue(lineSpace, "            </div>"));
                    lines.Add(GetLineValue(lineSpace, "        </div>"));
                }
                lines.Add(GetLineValue(lineSpace, "    </div>"));
            }
            lines.Add(GetLineValue());
            lines.Add(GetLineValue(lineSpace, "@{"));
            lines.Add(GetLineValue(lineSpace, $"        ActionService.RowId = Model.{model.KeyName};"));
            lines.Add(GetLineValue(lineSpace, "}"));
            lines.Add(GetLineValue(lineSpace, "    @using (Html.BeginForm())"));
            lines.Add(GetLineValue(lineSpace, "    {"));
            lines.Add(GetLineValue(lineSpace, $"        @Html.HiddenFor(model => model.{model.KeyName})"));
            lines.Add(GetLineValue(lineSpace, "        @await Html.PartialAsync(\"_PartialFormDelete\")"));
            lines.Add(GetLineValue(lineSpace, "    }"));
            lines.Add(GetLineValue(lineSpace, "</div>"));
            if (model.UseCard) AddCardFooter(ref lines, model);
            return lines;
        }
    }
}