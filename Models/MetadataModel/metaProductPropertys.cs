using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shopping.Models
{
    public partial class ProductPropertys
    {
        [NotMapped]
        public string? PropertyName { get; set; }
    }
}

public class z_metaProductPropertys
{
    [Key]
    public int Id { get; set; }

    public bool IsSelect { get; set; }

    public string? ProdNo { get; set; }

    public string? PropertyNo { get; set; }

    public string? PropertyValue { get; set; }

    public string? Remark { get; set; }
}