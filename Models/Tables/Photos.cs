using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Photos
{
    public int Id { get; set; }

    public string? CodeNo { get; set; }

    public string? FolderName { get; set; }

    public string? PhotoName { get; set; }

    public string? PriceName { get; set; }

    public DateTime SaleDate { get; set; }

    public string? DetailText { get; set; }

    public string? Remark { get; set; }
}
