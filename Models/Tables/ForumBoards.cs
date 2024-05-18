using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class ForumBoards
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public string? SortNo { get; set; }

    public string? BoardNo { get; set; }

    public string? BoardName { get; set; }

    public string? IconName { get; set; }

    public string? DescriptionText { get; set; }

    public string? Remark { get; set; }

    public string? GuidNo { get; set; }
}
