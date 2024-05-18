using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Departments
{
    public int Id { get; set; }

    public string? DeptNo { get; set; }

    public string? DeptName { get; set; }

    public string? Remark { get; set; }
}
