using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public partial class CodeCreate : CodeBaseClass
    {
        public List<string> CreateViewList(CodeGeneratorModel model)
        {
            using var codeService = new CodeGeneratorService();
            var PropertyList = new List<CodePropertyModel>();
            if (model.SelectAllColumn)
            {
                PropertyList = codeService.GetPropertyModelList(model.ModelName, model.ModelClassName, "z_meta");
                var data = PropertyList.Where(m => m.IsKeyColumn).FirstOrDefault();
                model.KeyName = (data == null) ? "" : data.PropertyName;
            }
            else
            {
                if (model.DisplayColumns.Count() > 0)
                {
                    foreach (var col in model.DisplayColumns)
                    {
                        PropertyList.Add(new CodePropertyModel()
                        {
                            PropertyName = col
                        });
                    }
                    model.KeyName = "";
                }
            }

            string lineEmpty = "";
            string lastName = "";
            string lineSpace = model.UseCard ? "        " : lineEmpty;
            List<string> lines = new List<string>();
            lines.Add(GetLineValue("", "@using X.PagedList.Mvc.Core;"));
            lines.Add(GetLineValue("", "@using X.PagedList;"));
            AddViewHeader(ref lines, model);
            if (model.UseCard) AddCardHeader(ref lines, model);
            lines.Add(GetLineValue(lineSpace, "<div class=\"overflow-scroll\">"));
            lines.Add(GetLineValue(lineSpace, "    <div class=\"row mx-1\">"));
            lines.Add(GetLineValue(lineSpace, "        <div class=\"col-md-12\">"));
            lines.Add(GetLineValue(lineSpace, "            <div class=\"float-start\">"));
            lines.Add(GetLineValue(lineSpace, "                @Html.PagedListPager((IPagedList)Model, x => Url.Action(\"Index\", ActionService.Controller, new { area = ActionService.Area, id = x}))"));
            lines.Add(GetLineValue(lineSpace, "            </div>"));
            lines.Add(GetLineValue(lineSpace, "            <div class=\"float-start page-info\">"));
            lines.Add(GetLineValue(lineSpace, "                @SessionService.PageInfo"));
            lines.Add(GetLineValue(lineSpace, "            </div>"));
            lines.Add(GetLineValue(lineSpace, "            <div class=\"float-end\">"));
            lines.Add(GetLineValue(lineSpace, "                @using (Html.BeginForm(\"Search\", ActionService.Controller, new { area = ActionService.Area }, FormMethod.Post))"));
            lines.Add(GetLineValue(lineSpace, "                {"));
            lines.Add(GetLineValue(lineSpace, "                    <table>"));
            lines.Add(GetLineValue(lineSpace, "                        <tr>"));
            lines.Add(GetLineValue(lineSpace, "                            <td>"));
            lines.Add(GetLineValue(lineSpace, "                                <span class=\"control-label fw-bold\">查詢條件：</span>"));
            lines.Add(GetLineValue(lineSpace, "                            </td>"));
            lines.Add(GetLineValue(lineSpace, "                            <td class=\"pe-2\">"));
            lines.Add(GetLineValue(lineSpace, "                                <input id=\"SearchText\" name=\"SearchText\" type=\"text\" class=\"form-control data-list-search-text\" placeholder=\"請輸入查詢條件...\""));
            lines.Add(GetLineValue(lineSpace, "value=\"@SessionService.SearchText\" style=\"width:200px;\" />"));
            lines.Add(GetLineValue(lineSpace, "                            </td>"));
            lines.Add(GetLineValue(lineSpace, "                            <td>"));
            lines.Add(GetLineValue(lineSpace, "                                <input type=\"submit\" value=\"查詢\" class=\"btn btn-primary\" />"));
            lines.Add(GetLineValue(lineSpace, "                            </td>"));
            lines.Add(GetLineValue(lineSpace, "                        </tr>"));
            lines.Add(GetLineValue(lineSpace, "                    </table>"));
            lines.Add(GetLineValue(lineSpace, "                }"));
            lines.Add(GetLineValue(lineSpace, "            </div>"));
            lines.Add(GetLineValue(lineSpace, "        </div>"));
            lines.Add(GetLineValue(lineSpace, "    </div>"));
            lines.Add(GetLineValue(lineSpace, "    <table class=\"table table-bordered\">"));
            lines.Add(GetLineValue(lineSpace, "        <thead class=\"table-secondary\">"));
            lines.Add(GetLineValue(lineSpace, "            <tr>"));
            lines.Add(GetLineValue(lineSpace, "                <th>"));
            lines.Add(GetLineValue(lineSpace, "                    @Html.ActionLinkButton(enFormLinkButton.CreateEdit , 0)"));
            lines.Add(GetLineValue(lineSpace, "                </th>"));
            foreach (var item in PropertyList)
            {
                if (item.PropertyName != model.KeyName)
                {
                    lines.Add(GetLineValue(lineSpace, "                <th>"));
                    lines.Add(GetLineValue(lineSpace, $"                    @Html.DisplayNameSortFor(model => model.{item.PropertyName})"));
                    lines.Add(GetLineValue(lineSpace, "                </th>"));
                    lastName = item.PropertyName;
                }
            }
            lines.Add(GetLineValue(lineSpace, "            </tr>"));
            lines.Add(GetLineValue(lineSpace, "        </thead>"));
            lines.Add(GetLineValue(lineSpace, "        <tbody>"));
            lines.Add(GetLineValue(lineSpace, "            @foreach (var item in Model)"));
            lines.Add(GetLineValue(lineSpace, "            {"));
            lines.Add(GetLineValue(lineSpace, "                ActionService.RowId = item.Id;"));
            lines.Add(GetLineValue(lineSpace, "                ActionService.RowData = (item." + CodeSessionService.DeleteName + " == null) ? \"\" : item." + CodeSessionService.DeleteNo + " + \" \" + item." + CodeSessionService.DeleteName + ";"));
            lines.Add(GetLineValue(lineSpace, "                <tr>"));
            lines.Add(GetLineValue(lineSpace, "                    <td>"));
            lines.Add(GetLineValue(lineSpace, "                        @Html.ActionLinkButton(enFormLinkButton.CreateEdit , item.Id)"));
            lines.Add(GetLineValue(lineSpace, "                        @Html.InputButton(enFormInputButton.DeleteAlert)"));
            lines.Add(GetLineValue(lineSpace, "                    </td>"));
            foreach (var item in PropertyList)
            {
                if (item.PropertyName != model.KeyName)
                {
                    if (item.PropertyName == lastName)
                        lines.Add(GetLineValue(lineSpace, "                    <td class=\"table-wrap\">"));
                    else
                        lines.Add(GetLineValue(lineSpace, "                    <td>"));
                    lines.Add(GetLineValue(lineSpace, "                        @Html.DisplayFor(modelItem => item." + item.PropertyName + ")"));
                    lines.Add(GetLineValue(lineSpace, "                    </td>"));
                }
            }
            lines.Add(GetLineValue(lineSpace, "                </tr>"));
            lines.Add(GetLineValue(lineSpace, "            }"));
            lines.Add(GetLineValue(lineSpace, "        </tbody>"));
            lines.Add(GetLineValue(lineSpace, "    </table>"));
            lines.Add(GetLineValue(lineSpace, "</div>"));
            if (model.UseCard) AddCardFooter(ref lines, model);
            return lines;
        }
    }
}