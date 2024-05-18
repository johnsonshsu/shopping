using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class News
{
    public int Id { get; set; }

    public string? CodeNo { get; set; }

    public DateTime PublishDate { get; set; }

    public string? HeaderName { get; set; }

    public string? DetailText { get; set; }

    public string? Remark { get; set; }
}
