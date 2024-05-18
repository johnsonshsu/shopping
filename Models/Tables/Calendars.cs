using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Calendars
{
    public int Id { get; set; }

    public string? TargetCode { get; set; }

    public string? TargetNo { get; set; }

    public string? CodeNo { get; set; }

    public string? SubjectName { get; set; }

    public DateTime StartDate { get; set; }

    public string? StartTime { get; set; }

    public DateTime EndDate { get; set; }

    public string? EndTime { get; set; }

    public string? ColorName { get; set; }

    public bool IsFullday { get; set; }

    public string? PlaceName { get; set; }

    public string? ContactName { get; set; }

    public string? ContactTel { get; set; }

    public string? PlaceAddress { get; set; }

    public string? RoomNo { get; set; }

    public string? ResourceText { get; set; }

    public string? Description { get; set; }

    public string? Remark { get; set; }
}
