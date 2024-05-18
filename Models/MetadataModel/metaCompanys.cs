using System;
using System.Collections.Generic;

namespace shopping.Models
{
    [ModelMetadataType(typeof(z_metaCompanys))]
    public partial class Companys
    {
        [Display(Name = "公司分類")]
        [NotMapped]
        public string? CodeName { get; set; }
    }
}

public class z_metaCompanys
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "預設")]
    public bool IsDefault { get; set; }
    [Display(Name = "啟用")]
    public bool IsEnabled { get; set; }
    [Display(Name = "分類代號")]
    public string? CodeNo { get; set; }
    [Display(Name = "公司編號")]
    public string? CompNo { get; set; }
    [Display(Name = "公司名稱")]
    public string? CompName { get; set; }
    [Display(Name = "公司簡稱")]
    public string? ShortName { get; set; }
    [Display(Name = "英文名稱")]
    public string? EngName { get; set; }
    [Display(Name = "英文簡稱")]
    public string? EngShortName { get; set; }
    [Display(Name = "登記日期")]
    public DateTime RegisterDate { get; set; }
    [Display(Name = "負責人")]
    public string? BossName { get; set; }
    [Display(Name = "連絡人")]
    public string? ContactName { get; set; }
    [Display(Name = "公司電話")]
    public string? CompTel { get; set; }
    [Display(Name = "連絡電話")]
    public string? ContactTel { get; set; }
    [Display(Name = "公司傳真")]
    public string? CompFax { get; set; }
    [Display(Name = "統一編號")]
    public string? CompID { get; set; }
    [Display(Name = "電子信箱")]
    public string? ContactEmail { get; set; }
    [Display(Name = "公司地址")]
    public string? CompAddress { get; set; }
    [Display(Name = "公司網址")]
    public string? CompUrl { get; set; }
    [Display(Name = "Twitter")]
    public string? TwitterUrl { get; set; }
    [Display(Name = "Facebook")]
    public string? FacebookUrl { get; set; }
    [Display(Name = "Instagram")]
    public string? InstagramUrl { get; set; }
    [Display(Name = "Skype")]
    public string? SkypeUrl { get; set; }
    [Display(Name = "Linkedin")]
    public string? LinkedinUrl { get; set; }
    [Display(Name = "緯度")]
    public decimal Latitude { get; set; }
    [Display(Name = "經度")]
    public decimal Longitude { get; set; }
    [Display(Name = "關於我們")]
    public string? AboutusText { get; set; }
    [Display(Name = "支援服務")]
    public string? SupportText { get; set; }
    [Display(Name = "退貨政策")]
    public string? ReturnText { get; set; }
    [Display(Name = "出貨政策")]
    public string? ShippingText { get; set; }
    [Display(Name = "付款方式")]
    public string? PaymentText { get; set; }
    [Display(Name = "備註說明")]
    public string? Remark { get; set; }
}