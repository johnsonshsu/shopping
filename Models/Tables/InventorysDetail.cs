using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class InventorysDetail
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string? ProductNo { get; set; }

    public int Qty { get; set; }

    public string? Remark { get; set; }
}
