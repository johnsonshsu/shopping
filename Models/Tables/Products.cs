using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Products
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsInventory { get; set; }

    public bool IsShowPhoto { get; set; }

    public string? ProdNo { get; set; }

    public string? ProdName { get; set; }

    public string? BrandName { get; set; }

    public string? BrandSeriesName { get; set; }

    public string? BarcodeNo { get; set; }

    public string? StatusNo { get; set; }

    public string? VendorNo { get; set; }

    public string? CategoryNo { get; set; }

    public int CostPrice { get; set; }

    public int MarketPrice { get; set; }

    public int SalePrice { get; set; }

    public int DiscountPrice { get; set; }

    public int InventoryQty { get; set; }

    public string? ContentText { get; set; }

    public string? SpecificationText { get; set; }

    public string? Remark { get; set; }
}
