using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class vi_CompVendor
{
    public int Id { get; set; }

    public bool IsDefault { get; set; }

    public bool IsEnabled { get; set; }

    public string? CodeNo { get; set; }

    public string? CompNo { get; set; }

    public string? CompName { get; set; }

    public string? ShortName { get; set; }

    public string? EngName { get; set; }

    public string? EngShortName { get; set; }

    public DateTime RegisterDate { get; set; }

    public string? BossName { get; set; }

    public string? ContactName { get; set; }

    public string? CompTel { get; set; }

    public string? ContactTel { get; set; }

    public string? CompFax { get; set; }

    public string? CompID { get; set; }

    public string? ContactEmail { get; set; }

    public string? CompAddress { get; set; }

    public string? CompUrl { get; set; }

    public string? TwitterUrl { get; set; }

    public string? FacebookUrl { get; set; }

    public string? InstagramUrl { get; set; }

    public string? SkypeUrl { get; set; }

    public string? LinkedinUrl { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string? AboutusText { get; set; }

    public string? SupportText { get; set; }

    public string? ReturnText { get; set; }

    public string? ShippingText { get; set; }

    public string? PaymentText { get; set; }

    public string? Remark { get; set; }
}
