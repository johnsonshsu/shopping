using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace shopping.Models
{
    [ModelMetadataType(typeof(z_metaOrders))]
    public partial class Orders
    {
        [NotMapped]
        public string? StatusName { get; set; }
        [NotMapped]
        public string? PaymentName { get; set; }
        [NotMapped]
        public string? ShippingName { get; set; }
    }
}

public class z_metaOrders
{
    [Key]
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