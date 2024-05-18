using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class AddressBooks
{
    public int Id { get; set; }

    public string? UserNo { get; set; }

    public string? CodeNo { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EngName { get; set; }

    public string? GenderCode { get; set; }

    public DateTime? Birthday { get; set; }

    public string? CompName { get; set; }

    public string? CompID { get; set; }

    public string? DeptName { get; set; }

    public string? TitleName { get; set; }

    public string? CompTel { get; set; }

    public string? ContactTel { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactAddress { get; set; }

    public string? LineID { get; set; }

    public string? FacebookID { get; set; }

    public string? TwitterID { get; set; }

    public string? InstagramID { get; set; }

    public string? LinkedInID { get; set; }

    public string? Remark { get; set; }
}
