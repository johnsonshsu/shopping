using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Carts
{
    public int Id { get; set; }

    public string? LotNo { get; set; }

    public string? MemberNo { get; set; }

    public string? VendorNo { get; set; }

    public string? CategoryNo { get; set; }

    public string? CategoryName { get; set; }

    public string? ProdNo { get; set; }

    public string? ProdName { get; set; }

    public string? ProdSpec { get; set; }

    public int OrderQty { get; set; }

    public int OrderPrice { get; set; }

    public int OrderAmount { get; set; }

    public DateTime CreateTime { get; set; }

    public string? Remark { get; set; }
}
