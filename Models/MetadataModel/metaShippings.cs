using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace shopping.Models
{
    [ModelMetadataType(typeof(z_metaShippings))]
    public partial class Shippings
    {

    }
}

public class z_metaShippings
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "運送編號")]
    public string? ShippingNo { get; set; }
    [Display(Name = "運送名稱")]
    public string? ShippingName { get; set; }
    [Display(Name = "備註")]
    public string? Remark { get; set; }
}