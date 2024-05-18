using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class PricingDetails
{
    public int Id { get; set; }

    public string? PricingNo { get; set; }

    public bool IsSupport { get; set; }

    public string? SortNo { get; set; }

    public string? ItemName { get; set; }

    public string? Remark { get; set; }
}
