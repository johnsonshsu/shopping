using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Pricings
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsAdvanced { get; set; }

    public bool IsRecommend { get; set; }

    public string? SortNo { get; set; }

    public string? PricingNo { get; set; }

    public string? PricingName { get; set; }

    public int ProdPrice { get; set; }

    public string? CycleName { get; set; }

    public string? Remark { get; set; }
}
