using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace shopping.Models
{
    [ModelMetadataType(typeof(z_metaProducts))]
    public partial class Products
    {
        [NotMapped]
        [Display(Name = "分類")]
        public string? CategoryName { get; set; }
        [NotMapped]
        [Display(Name = "狀態")]
        public string? StatusName { get; set; }
    }
}

public class z_metaProducts
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "上架")]
    public bool IsEnabled { get; set; }
    [Display(Name = "管理庫存")]
    public bool IsInventory { get; set; }
    [Display(Name = "顯示圖片")]
    public bool IsShowPhoto { get; set; }
    [Display(Name = "商品編號")]
    public string? ProdNo { get; set; }
    [Display(Name = "商品名稱")]
    public string? ProdName { get; set; }
    [Display(Name = "廠牌")]
    public string? BrandName { get; set; }
    [Display(Name = "系列名稱")]
    public string? BrandSeriesName { get; set; }
    [Display(Name = "條碼編號")]
    public string? BarcodeNo { get; set; }
    [Display(Name = "狀態編號")]
    public string? StatusNo { get; set; }
    [Display(Name = "廠商編號")]
    public string? VendorNo { get; set; }
    [Display(Name = "分類代號")]
    public string? CategoryNo { get; set; }
    [Display(Name = "成本價")]
    public int CostPrice { get; set; }
    [Display(Name = "市場價")]
    public int MarketPrice { get; set; }
    [Display(Name = "銷售價")]
    public int SalePrice { get; set; }
    [Display(Name = "折扣價")]
    public int DiscountPrice { get; set; }
    [Display(Name = "庫存量")]
    public int InventoryQty { get; set; }
    [Display(Name = "詳細說明")]
    public string? ContentText { get; set; }
    [Display(Name = "商品簡述")]
    public string? SpecificationText { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}
