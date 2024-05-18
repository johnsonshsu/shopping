using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Programs
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsPageSize { get; set; }

    public bool IsSearch { get; set; }

    public string? RoleNo { get; set; }

    public string? ModuleNo { get; set; }

    public string? SortNo { get; set; }

    public string? PrgNo { get; set; }

    public string? PrgName { get; set; }

    public string? CodeNo { get; set; }

    public string? AreaName { get; set; }

    public string? ControllerName { get; set; }

    public string? ActionName { get; set; }

    public string? ParmValue { get; set; }

    public int PageSize { get; set; }

    public string? Remark { get; set; }
}
