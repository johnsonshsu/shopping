using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class FormMaster
{
    public int Id { get; set; }

    public string? FormCode { get; set; }

    public string? UserNo { get; set; }

    public string? StatusCode { get; set; }

    public string? FormNo { get; set; }

    public DateTime? FormDate { get; set; }

    public DateTime? FormTime { get; set; }

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

    public DateTime? ApproveTime { get; set; }

    public DateTime? RejectTime { get; set; }

    public string? SourceNo { get; set; }

    public string? ApproveNo { get; set; }

    public string? RejectNo { get; set; }

    public string? NextNo { get; set; }

    public string? GuidNo { get; set; }

    public string? NotifyKey { get; set; }

    public string? FormDescribe { get; set; }

    public string? Remark { get; set; }
}
