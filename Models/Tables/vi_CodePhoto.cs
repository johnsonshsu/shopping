using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class vi_CodePhoto
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public string? BaseNo { get; set; }

    public string? ParentNo { get; set; }

    public string? SortNo { get; set; }

    public string? CodeNo { get; set; }

    public string? CodeName { get; set; }

    public string? CodeValue { get; set; }

    public string? Remark { get; set; }
}
