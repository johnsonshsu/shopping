using System.Net;
using System.Net.Mail;

public class GmailService : BaseClass
{
    private void Memo()
    {
        // 在 appsettings.json 中加入以下設定
        //"Gmail": {
        //  "UserName": "johnson.ezmail@gmail.com",
        //  "AppPassword": "xxxxxxxxxxx",
        //  "SenderEmail": "johnson.ezmail@gmail.com",
        //  "SenderName": "系統管理員",
        //  "HostUrl": "smtp.gmail.com",
        //  "Port": 587,
        //  "UseSSL": true
        //},
    }
    public GmailService()
    {
        MessageText = "";
        Subject = "";
        Body = "";
        InitComponent();
    }
    private void InitComponent()
    {
        // 初始化 Gamil 設定
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        var config = builder.Build();
        UserName = config.GetSection("Gmail").GetValue<string>("UserName") ?? "";
        AppPassword = config.GetSection("Gmail").GetValue<string>("AppPassword") ?? "";
        SenderEmail = config.GetSection("Gmail").GetValue<string>("SenderEmail") ?? "";
        SenderName = config.GetSection("Gmail").GetValue<string>("SenderName") ?? "";
        HostUrl = config.GetSection("Gmail").GetValue<string>("HostUrl") ?? "";
        HostPort = (int)config.GetSection("Gmail").GetValue<int>("Port");
        UseSSL = (bool)config.GetSection("Gmail").GetValue<bool>("UseSSL");
    }
    #region 屬性
    /// <summary>
    /// 訊息文字
    /// </summary>
    public string MessageText { get; set; } = "";
    /// <summary>
    /// 寄件者姓名
    /// </summary>
    public string SenderName { get; set; } = "";
    /// <summary>
    /// 寄件者電子信箱
    /// </summary>
    public string SenderEmail { get; set; } = "";
    /// <summary>
    /// 寄件者 (Google 帳號)
    /// </summary>
    public string UserName { get; set; } = "";
    /// <summary>
    /// Google 帳號應用程式密碼
    /// </summary>
    public string AppPassword { get; set; } = "";
    /// <summary>
    /// 收件者姓名
    /// </summary>
    public string ReceiverName { get; set; } = "";
    /// <summary>
    /// 收件者 Email
    /// </summary>
    public string ReceiverEmail { get; set; } = "";
    /// <summary>
    /// 信件主旨
    /// </summary>
    public string Subject { get; set; } = "";
    /// <summary>
    /// 信件內文
    /// </summary>
    public string Body { get; set; } = "";
    /// <summary>
    /// 寄件伺服器位址
    /// </summary>
    public string HostUrl { get; set; } = "";
    /// <summary>
    /// 通訊連接埠號碼
    /// </summary>
    public int HostPort { get; set; }
    /// <summary>
    /// 啟用 SSL 機制
    /// </summary>
    public bool UseSSL { get; set; }
    #endregion
    #region 事件
    /// <summary>
    /// 送出信件
    /// </summary>
    public void Send()
    {
        var userEmail = new MailAddress(UserName, SenderName);
        var fromEmail = new MailAddress(SenderEmail, SenderName);
        var toEmail = new MailAddress(ReceiverEmail);
        try
        {
            var smtp = new SmtpClient
            {
                Host = HostUrl,
                Port = HostPort,
                EnableSsl = UseSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userEmail.Address, AppPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        catch (Exception ex)
        {
            MessageText = ex.Message;
        }
    }
    #endregion
}

/// <summary>
/// 電子信箱資料格式
/// </summary>
public class MailObject
{
    /// <summary>
    /// 寄件者名稱
    /// </summary>
    /// <value></value>
    public DateTime MailTime { get; set; } = DateTime.Now;
    /// <summary>
    /// 寄件者名稱
    /// </summary>
    /// <value></value>
    public string FromName { get; set; } = "";
    /// <summary>
    /// 寄件者電子信箱
    /// </summary>
    /// <value></value>
    public string FromEmail { get; set; } = "";
    /// <summary>
    /// 收件者名稱
    /// </summary>
    /// <value></value>
    public string ToName { get; set; } = "";
    /// <summary>
    /// 收件者電子信箱
    /// </summary>
    /// <value></value>
    public string ToEmail { get; set; } = "";
    /// <summary>
    /// 信件主旨
    /// </summary>
    /// <value></value>
    public string Subject { get; set; } = "";
    /// <summary>
    /// 使用者編號
    /// </summary>
    /// <value></value>
    public string UserNo { get; set; } = "";
    /// <summary>
    /// 使用者名稱
    /// </summary>
    /// <value></value>
    public string UserName { get; set; } = "";
    /// <summary>
    /// 使用者密碼
    /// </summary>
    /// <value></value>
    public string Password { get; set; } = "";
    /// <summary>
    /// 驗證碼
    /// </summary>
    /// <value></value>
    public string ValidateCode { get; set; } = "";
    /// <summary>
    /// 返回網址
    /// </summary>
    /// <value></value>
    public string ReturnUrl { get; set; } = "";
    /// <summary>
    /// 電子郵內主旨
    /// </summary>
    /// <value></value>
    public string MailSubject { get; set; } = "";
    /// <summary>
    /// 電子郵內文件
    /// </summary>
    /// <value></value>
    public string MailContent { get; set; } = "";
}