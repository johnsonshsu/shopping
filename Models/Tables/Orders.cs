using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Orders
{
    public int Id { get; set; }

    public string? SheetNo { get; set; }

    public DateTime SheetDate { get; set; }

    public string? StatusCode { get; set; }

    public bool IsClosed { get; set; }

    public bool IsValid { get; set; }

    public string? CustNo { get; set; }

    public string? CustName { get; set; }

    public string? PaymentNo { get; set; }

    public string? ShippingNo { get; set; }

    public string? ReceiverName { get; set; }

    public string? ReceiverEmail { get; set; }

    public string? ReceiverAddress { get; set; }

    public int OrderAmount { get; set; }

    public int TaxAmount { get; set; }

    public int TotalAmount { get; set; }

    public string? Remark { get; set; }

    public string? GuidNo { get; set; }
}
