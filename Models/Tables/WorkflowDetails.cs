using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class WorkflowDetails
{
    public int Id { get; set; }

    public bool IsClose { get; set; }

    public bool IsApprove { get; set; }

    public bool IsReject { get; set; }

    public string? MasterGuidNo { get; set; }

    public string? RouteGuidNo { get; set; }

    public string? RouteOrder { get; set; }

    public string? RoleNo { get; set; }

    public string? RoleName { get; set; }

    public string? UserNo { get; set; }

    public string? UserName { get; set; }

    public string? AgentNo { get; set; }

    public string? AgentName { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime? UserReadTime { get; set; }

    public DateTime? AgentReadTime { get; set; }

    public string? SignUserNo { get; set; }

    public string? SignUserName { get; set; }

    public DateTime? SignTime { get; set; }

    public string? SignComment { get; set; }

    public string? Remark { get; set; }

    public string? GuidNo { get; set; }
}
