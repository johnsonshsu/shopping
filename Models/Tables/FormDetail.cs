using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class FormDetail
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string? FormCode { get; set; }

    public string? FormNo { get; set; }

    public string? GuidNo { get; set; }

    public string? TargetNo { get; set; }

    public string? TargetName { get; set; }

    public string? DeptNo { get; set; }

    public string? DeptName { get; set; }

    public string? TitleNo { get; set; }

    public string? TitleName { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? EndTime { get; set; }

    public string? CodeNo { get; set; }

    public string? CodeName { get; set; }

    public int Qty1 { get; set; }

    public int Qty2 { get; set; }

    public string? Remark { get; set; }
}
