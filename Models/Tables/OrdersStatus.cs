using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class OrdersStatus
{
    public int Id { get; set; }

    public bool IsPayed { get; set; }

    public bool IsClosed { get; set; }

    public string? StatusNo { get; set; }

    public string? StatusName { get; set; }

    public string? Remark { get; set; }
}
