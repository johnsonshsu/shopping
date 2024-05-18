using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Vacations
{
    public int Id { get; set; }

    public int VacYear { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? CodeNo { get; set; }

    public string? Remark { get; set; }
}
