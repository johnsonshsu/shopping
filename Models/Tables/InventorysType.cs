using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class InventorysType
{
    public int Id { get; set; }

    public string? TypeNo { get; set; }

    public string? TypeName { get; set; }

    public int QtyCode { get; set; }

    public string? Remark { get; set; }
}
