using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Securitys
{
    public int Id { get; set; }

    public string? RoleNo { get; set; }

    public string? TargetNo { get; set; }

    public string? ModuleNo { get; set; }

    public string? PrgNo { get; set; }

    public bool IsAdd { get; set; }

    public bool IsEdit { get; set; }

    public bool IsDelete { get; set; }

    public bool IsConfirm { get; set; }

    public bool IsUndo { get; set; }

    public bool IsInvalid { get; set; }

    public bool IsUpload { get; set; }

    public bool IsDownload { get; set; }

    public bool IsPrint { get; set; }

    public string? Remark { get; set; }
}
