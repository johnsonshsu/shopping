using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Employees
{
    public int Id { get; set; }

    public bool IsValid { get; set; }

    public string? EmpNo { get; set; }

    public string? EmpName { get; set; }

    public string? GenderCode { get; set; }

    public string? DeptNo { get; set; }

    public string? TitleNo { get; set; }

    public DateTime? Birthday { get; set; }

    public DateTime? OnboardDate { get; set; }

    public DateTime? LeaveDate { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactTel { get; set; }

    public string? ContactAddress { get; set; }

    public string? Remark { get; set; }
}
