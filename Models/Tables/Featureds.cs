using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Featureds
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public string? SortNo { get; set; }

    public string? ProdNo { get; set; }

    public string? Remark { get; set; }
}
