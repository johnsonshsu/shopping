using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class WorkflowMasters
{
    public int Id { get; set; }

    public bool IsClose { get; set; }

    public bool IsApprove { get; set; }

    public bool IsReject { get; set; }

    public string? FlowGuidNo { get; set; }

    public string? SheetNo { get; set; }

    public string? SheetName { get; set; }

    public string? UserNo { get; set; }

    public string? UserName { get; set; }

    public DateTime DeadlineTime { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? ContentText { get; set; }

    public string? Remark { get; set; }

    public string? GuidNo { get; set; }
}
