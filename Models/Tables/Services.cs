using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Services
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public string? SortNo { get; set; }

    public string? HeaderName { get; set; }

    public string? DetailName { get; set; }

    public string? ImageUrl { get; set; }

    public string? Remark { get; set; }
}
