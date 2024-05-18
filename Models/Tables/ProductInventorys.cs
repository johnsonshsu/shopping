using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class ProductInventorys
{
    public int Id { get; set; }

    public string? ProdNo { get; set; }

    public string? PropertyNo { get; set; }

    public bool IsInventory { get; set; }

    public int InventoryQty { get; set; }

    public string? Remark { get; set; }
}
