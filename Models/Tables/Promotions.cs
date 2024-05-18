using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Promotions
{
    public int Id { get; set; }

    public string? SortNo { get; set; }

    public string? ProdNo { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int SalePrice { get; set; }

    public string? Remark { get; set; }
}
