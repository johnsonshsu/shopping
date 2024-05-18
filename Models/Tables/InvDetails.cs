using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class InvDetails
{
    public int Id { get; set; }

    public string? WareHouseNo { get; set; }

    public string? ProductNo { get; set; }

    public int Qty { get; set; }

    public string? Remark { get; set; }
}
