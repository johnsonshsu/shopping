using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Teams
{
    public int Id { get; set; }

    public string? SortNo { get; set; }

    public string? TeamNo { get; set; }

    public string? TeamName { get; set; }

    public string? EngName { get; set; }

    public string? GenderCode { get; set; }

    public string? DeptName { get; set; }

    public string? TitleName { get; set; }

    public string? TwitterUrl { get; set; }

    public string? FacebookUrl { get; set; }

    public string? LinkedinUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public string? SkypeUrl { get; set; }

    public string? ContactEmail { get; set; }

    public string? DetailText { get; set; }

    public string? Remark { get; set; }
}
