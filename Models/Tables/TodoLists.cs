using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class TodoLists
{
    public int Id { get; set; }

    public bool IsFinished { get; set; }

    public string? UserNo { get; set; }

    public string? CodeNo { get; set; }

    public string? TitleName { get; set; }

    public DateTime DeadlineDate { get; set; }

    public string? Remark { get; set; }
}
