using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Logs
{
    public int Id { get; set; }

    public DateTime LogDate { get; set; }

    public DateTime LogTime { get; set; }

    public string? CodeNo { get; set; }

    public string? UserNo { get; set; }

    public string? UserName { get; set; }

    public string? TargetNo { get; set; }

    public string? LogNo { get; set; }

    public int LogQty { get; set; }

    public string? Remark { get; set; }
}
