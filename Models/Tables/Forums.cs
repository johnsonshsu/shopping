using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Forums
{
    public int Id { get; set; }

    public string? ParentGuid { get; set; }

    public string? ReplyGuid { get; set; }

    public string? BoardNo { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsClosed { get; set; }

    public DateTime SubjectDate { get; set; }

    public string? UserNo { get; set; }

    public string? SubjectName { get; set; }

    public string? SubjectContent { get; set; }

    public string? Remark { get; set; }

    public string? GuidNo { get; set; }
}
