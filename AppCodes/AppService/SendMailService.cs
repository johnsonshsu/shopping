public class SendMailService : BaseClass
{
    /// <summary>
    /// 會員註冊寄發驗證的電子郵件
    /// </summary>
    /// <param name="mailObject">電子郵件物件</param>
    /// <returns></returns>
    public string UserRegister(MailObject mailObject)
    {
        using var gmail = new GmailService();
        using var dpr = new DapperRepository();

        //變數
        string str_reg_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

        //信件內容
        gmail.MessageText = "";
        gmail.ReceiverName = mailObject.UserName;
        gmail.ReceiverEmail = mailObject.ToEmail;
        gmail.Subject = string.Format("{0} 會員註冊驗證通知信", AppService.AppName);
        gmail.Body = string.Format("敬愛的會員 {0} 您好!! <br /><br />", mailObject.UserName);
        gmail.Body += string.Format("您於 {0} 在我們網站註冊了會員帳號<br />", str_reg_date);
        gmail.Body += string.Format("您的會員帳號為：{0}<br />", mailObject.UserNo);
        gmail.Body += "請您點擊以下連結進行帳號電子郵件驗證<br /><br />";
        gmail.Body += string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br /><br />", mailObject.ReturnUrl, mailObject.ReturnUrl);
        gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
        gmail.Body += "-------------------------------------------<br />";
        gmail.Body += string.Format("{0} {1}<br />", AppService.AppName, AppService.AppVersion);
        gmail.Body += string.Format("{0}<br />", ActionService.HttpHost);
        gmail.Body += "-------------------------------------------<br />";
        //寄信
        gmail.Send();
        return gmail.MessageText;
    }


    /// <summary>
    /// 帳號忘記密碼寄發密碼重設的電子郵件
    /// </summary>
    /// <param name="mailObject">電子郵件物件</param>
    /// <returns></returns>
    public string UserForget(MailObject mailObject)
    {
        using var gmail = new GmailService();
        using var dpr = new DapperRepository();

        //變數
        string str_reg_date = mailObject.MailTime.ToString("yyyy/MM/dd HH:mm");

        //信件內容
        gmail.MessageText = "";
        gmail.ReceiverName = mailObject.UserName;
        gmail.ReceiverEmail = mailObject.ToEmail;
        gmail.Subject = string.Format("{0} 帳號忘記密碼重新設定通知信", AppService.AppName);
        gmail.Body = string.Format("敬愛的會員 {0} 您好!! <br /><br />", mailObject.UserName);
        gmail.Body += string.Format("您於 {0} 在我們網站執行了忘記密碼的功能，<br /><br />", str_reg_date);
        gmail.Body += string.Format("您新的密碼為： {0} <br /><br />", mailObject.Password);
        gmail.Body += "請您點擊以下連結進行忘記密碼驗證，並再自行變更您熟悉的密碼！！<br /><br />";
        gmail.Body += string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br /><br />", mailObject.ReturnUrl, mailObject.ReturnUrl);
        gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
        gmail.Body += "-------------------------------------------<br />";
        gmail.Body += string.Format("{0} {1}<br />", AppService.AppName, AppService.AppVersion);
        gmail.Body += string.Format("{0}<br />", ActionService.HttpHost);
        gmail.Body += "-------------------------------------------<br />";
        //寄信
        gmail.Send();
        return gmail.MessageText;
    }

    /// <summary>
    /// 帳號重設密碼寄發驗證的電子郵件
    /// </summary>
    /// <param name="mailObject">電子郵件物件</param>
    /// <returns></returns>
    public string UserResetPassword(MailObject mailObject)
    {
        using var gmail = new GmailService();
        using var dpr = new DapperRepository();

        //變數
        string str_reg_date = DateTime.Now.ToString("yyyy/MM/dd HH:mm");

        //信件內容
        gmail.MessageText = "";
        gmail.ReceiverName = mailObject.UserName;
        gmail.ReceiverEmail = mailObject.ToEmail;
        gmail.Subject = string.Format("{0} 帳號重設密碼重新設定通知信", AppService.AppName);
        gmail.Body = string.Format("敬愛的會員 {0} 您好!! <br /><br />", mailObject.UserName);
        gmail.Body += string.Format("您於 {0} 在我們網站執行了重設密碼的功能，<br /><br />", str_reg_date);
        gmail.Body += "請您點擊以下連結完成兩階段驗證！！<br /><br />";
        gmail.Body += string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a><br /><br />", mailObject.ReturnUrl, mailObject.ReturnUrl);
        gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
        gmail.Body += "-------------------------------------------<br />";
        gmail.Body += string.Format("{0} {1}<br />", AppService.AppName, AppService.AppVersion);
        gmail.Body += string.Format("{0}<br />", ActionService.HttpHost);
        gmail.Body += "-------------------------------------------<br />";
        //寄信
        gmail.Send();
        return gmail.MessageText;
    }

    /// <summary>
    ///連絡我們的電子郵件
    /// </summary>
    /// <param name="mailObject">電子郵件物件</param>
    /// <returns></returns>
    public string ContactUs(MailObject mailObject)
    {
        using var gmail = new GmailService();

        //寄信給管理員
        gmail.MessageText = "";
        gmail.ReceiverName = AppService.AdminName;
        gmail.ReceiverEmail = AppService.AdminEmail;
        gmail.Subject = string.Format("{0} 連絡我們的通知信-{1}", AppService.AppName, mailObject.ToName);
        gmail.Body = string.Format("敬愛的 {0} 您好!! <br /><br />", mailObject.FromName);
        gmail.Body += string.Format("{0} 在我們網站 {1} 提交了一封連絡訊息，<br /><br />", mailObject.ToName, AppService.AppName);
        gmail.Body += string.Format("訊息的內容如下：<br /><br />");
        gmail.Body += string.Format("訊息提交時間： {0} <br />", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
        gmail.Body += string.Format("提訊人姓名： {0} <br />", mailObject.ToName);
        gmail.Body += string.Format("提訊人信箱： {0} <br />", mailObject.ToEmail);
        gmail.Body += string.Format("訊息主旨： {0} <br />", mailObject.MailSubject);
        gmail.Body += string.Format("訊息內文：<br /></hr>");
        gmail.Body += mailObject.MailContent;
        gmail.Body += "<br /><br />";
        gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
        gmail.Body += "-------------------------------------------<br />";
        gmail.Body += string.Format("{0} {1}<br />", AppService.AppName, AppService.AppVersion);
        gmail.Body += string.Format("{0}<br />", ActionService.HttpHost);
        gmail.Body += "-------------------------------------------<br />";
        //寄信
        gmail.Send();
        if (!string.IsNullOrEmpty(gmail.MessageText)) return gmail.MessageText;

        //寄信給提交訊息的人員備查
        gmail.MessageText = "";
        gmail.ReceiverName = mailObject.ToName;
        gmail.ReceiverEmail = mailObject.ToEmail;
        gmail.Subject = string.Format("{0} 連絡我們訊息已提交, 請靜待回覆!!", AppService.AppName);
        gmail.Body = string.Format("敬愛的 {0} 您好!! <br /><br />", mailObject.ToName);
        gmail.Body += string.Format("您在我們網站 {0} 提交了一封連絡訊息，<br /><br />", AppService.AppName);
        gmail.Body += string.Format("訊息的內容如下：<br /><br />");
        gmail.Body += string.Format("訊息提交時間： {0} <br />", DateTime.Now.ToString("yyyy/MM/dd HH:mm"));
        gmail.Body += string.Format("提訊人姓名： {0} <br />", mailObject.ToName);
        gmail.Body += string.Format("提訊人信箱： {0} <br />", mailObject.ToEmail);
        gmail.Body += string.Format("訊息主旨： {0} <br />", mailObject.MailSubject);
        gmail.Body += string.Format("訊息內文：<br /></hr>");
        gmail.Body += mailObject.MailContent;
        gmail.Body += "<br /><br />";
        gmail.Body += "我們已收到您的來信，將會在最短的時間內給您回覆，感謝您的來信!!<br /><br />";
        gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
        gmail.Body += "-------------------------------------------<br />";
        gmail.Body += string.Format("{0}<br />", AppService.AppName);
        gmail.Body += "-------------------------------------------<br />";
        //寄信
        gmail.Send();

        return gmail.MessageText;
    }

    /// <summary>
    ///訂閱
    /// </summary>
    /// <param name="email">電子信箱</param>
    /// <param name="isAddEmail">是否訂閱/退訂</param>
    /// <returns></returns>
    public string Subscription(string email, bool isAddEmail)
    {
        using var gmail = new GmailService();

        //寄信給管理員
        string str_data = (isAddEmail) ? "訂閱" : "退訂";
        gmail.MessageText = "";
        gmail.ReceiverName = AppService.AdminName;
        gmail.ReceiverEmail = AppService.AdminEmail;
        gmail.Subject = string.Format("{0} {1}網站的通知信", AppService.AppName, str_data);
        gmail.Body = string.Format("敬愛的 {0} 您好!! <br /><br />", AppService.AdminName);
        gmail.Body += string.Format("我們網站 {0} 有人提交了一份{1}資訊，<br /><br />", AppService.AppName, str_data);
        gmail.Body += string.Format("訂閱人信箱： {0} <br />", email);
        gmail.Body += "<br /><br />";
        gmail.Body += "本信件為系統自動寄出,請勿回覆!!<br /><br />";
        gmail.Body += "-------------------------------------------<br />";
        gmail.Body += string.Format("{0}<br />", AppService.AppName);
        gmail.Body += "-------------------------------------------<br />";
        //寄信
        gmail.Send();
        return gmail.MessageText;
    }
}