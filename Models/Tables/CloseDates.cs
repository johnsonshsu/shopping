using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class CloseDates
{
    public int Id { get; set; }

    public string? CodeNo { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Remark { get; set; }
}
