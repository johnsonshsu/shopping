using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Carousels
{
    public int Id { get; set; }

    public bool IsActive { get; set; }

    public string? SortNo { get; set; }

    public string? HeaderName { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? MoreUrl { get; set; }

    public string? Remark { get; set; }
}
