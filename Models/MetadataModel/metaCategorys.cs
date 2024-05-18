using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace shopping.Models
{
    [ModelMetadataType(typeof(z_metaCategorys))]
    public partial class Categorys
    {

    }
}

public class z_metaCategorys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "分類")]
    public bool IsCategory { get; set; }
    [Display(Name = "父階編號")]
    public string? ParentNo { get; set; }
    [Display(Name = "分類代號")]
    public string? CategoryNo { get; set; }
    [Display(Name = "排序")]
    public string? SortNo { get; set; }
    [Display(Name = "分類名稱")]
    public string? CategoryName { get; set; }
    [Display(Name = "分類路徑")]
    public string? RouteName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}