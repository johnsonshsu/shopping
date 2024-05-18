using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class ProductTags
{
    public int Id { get; set; }

    public string? ProdNo { get; set; }

    public string? TagName { get; set; }

    public string? Remark { get; set; }
}
