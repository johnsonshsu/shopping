using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


public class vmOrders
{
    [Display(Name = "會員姓名")]
    public string? MemberName { get; set; } = "";
    [Display(Name = "會員信箱")]
    public string? MemberEmail { get; set; } = "";
    [Display(Name = "會員電話")]
    public string? MemberTel { get; set; } = "";
    [Display(Name = "會員地址")]
    public string? MemberAddress { get; set; } = "";
    [Display(Name = "收件人資料與購買人資料不同")]
    public string? IsDiffenceMember { get; set; } = "on";
    [Display(Name = "收件人姓名")]
    [Required(ErrorMessage = "收件人姓名不可空白")]
    public string? ReceiveName { get; set; } = "";
    [Display(Name = "收件人信箱")]
    [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入電子信箱")]
    public string? ReceiveEmail { get; set; } = "";
    [Display(Name = "收件人電話")]
    [Required(ErrorMessage = "收件人電話不可空白")]
    public string? ReceiveTel { get; set; } = "";
    [Display(Name = "收件人地址")]
    [Required(ErrorMessage = "收件人地址不可空白")]
    public string? ReceiveAddress { get; set; } = "";
    [Display(Name = "付款方式")]
    [Required(ErrorMessage = "付款方式不可空白")]
    public string? PaymentNo { get; set; } = "";
    [Display(Name = "運送方式")]
    [Required(ErrorMessage = "運送方式不可空白")]
    public string? ShippingNo { get; set; } = "";
    [Display(Name = "訂單備註")]
    public string? Remark { get; set; } = "";
}