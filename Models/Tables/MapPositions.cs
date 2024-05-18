using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class MapPositions
{
    public int Id { get; set; }

    public string? TargetCode { get; set; }

    public string? TargetNo { get; set; }

    public string? TitleName { get; set; }

    public string? ContactName { get; set; }

    public string? ContactTel { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactAddress { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string? Remark { get; set; }
}
