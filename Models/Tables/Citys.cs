using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Citys
{
    public int Id { get; set; }

    public string? SortNo { get; set; }

    public string? CityName { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? Remark { get; set; }
}
