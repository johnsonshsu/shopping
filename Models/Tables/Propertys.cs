using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Propertys
{
    public int Id { get; set; }

    public bool IsSelect { get; set; }

    public string? PropertyNo { get; set; }

    public string? PropertyName { get; set; }

    public string? PropertyValue { get; set; }

    public string? Remark { get; set; }
}
