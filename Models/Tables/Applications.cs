using System;
using System.Collections.Generic;

namespace shopping.Models;

public partial class Applications
{
    public int Id { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsDebug { get; set; }

    public string? AppName { get; set; }

    public string? AdminName { get; set; }

    public string? ShopName { get; set; }

    public string? AppVersion { get; set; }

    public bool EncryptionMode { get; set; }

    public string? PowerBy { get; set; }

    public string? LanguageNo { get; set; }

    public string? GoogleMapKey { get; set; }

    public string? MailSenderName { get; set; }

    public string? MailSenderEmail { get; set; }

    public string? MailReceiverName { get; set; }

    public string? MailReceiverEmail { get; set; }

    public string? MailAppPassword { get; set; }

    public string? MailHostUrl { get; set; }

    public int MailHostPort { get; set; }

    public bool MailUseSSL { get; set; }

    public string? WebSiteUrl { get; set; }

    public string? Remark { get; set; }
}
