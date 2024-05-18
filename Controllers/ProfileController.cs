using Microsoft.AspNetCore.Mvc;

namespace shopping.Controllers
{
    public class ProfileController : Controller
    {
        [HttpGet]
        [Login()]
        public ActionResult Index()
        {
            ActionService.SetActionData("我的帳號", "", "使用者");
            using var user = new z_sqlUsers();
            var model = user
                .GetDataList()
                .Where(m => m.UserNo == SessionService.UserNo)
                .FirstOrDefault();
            return View(model);
        }

        [HttpGet]
        [Login()]
        public ActionResult Edit()
        {
            ActionService.SetActionData("修改個人資料", "", "我的帳號");
            using var user = new z_sqlUsers();
            var model = user
                .GetDataList()
                .Where(m => m.UserNo == SessionService.UserNo)
                .FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        [Login()]
        public ActionResult Edit(Users model)
        {
            if (!ModelState.IsValid) return View(model);
            using var user = new z_sqlUsers();
            user.UpdateUserProfile(model);
            return RedirectToAction("Index", "Profile", new { area = "" });
        }

        [HttpGet]
        [Login()]
        public IActionResult ResetPassword()
        {
            ActionService.SetActionData("重設密碼", "", "使用者");
            vmResetPassword model = new vmResetPassword();
            return View(model);
        }

        [HttpPost]
        [Login()]
        public IActionResult ResetPassword(vmResetPassword model)
        {
            //1.檢查輸入資料是否合格
            if (!ModelState.IsValid) return View(model);
            using var user = new z_sqlUsers();

            //2.檢查帳號是否存在,存在則設定新的密碼也設定狀態為未審核
            string str_code = user.ResetPassword(model);
            if (string.IsNullOrEmpty(str_code))
            {
                ModelState.AddModelError("UserNo", "目前密碼不正確!!");
                return View(model);
            }

            //3.寄出忘記密碼驗證信
            using var sendEmail = new SendMailService();

            string str_message = user.CheckMailValidateCode(str_code);
            if (string.IsNullOrEmpty(str_message))
            {
                var userData = user.GetValidateUser(str_code);
                var mailObject = new MailObject();
                mailObject.MailTime = DateTime.Now;
                mailObject.ValidateCode = str_code;
                mailObject.UserNo = userData.UserNo;
                mailObject.UserName = userData.UserName;
                mailObject.ToName = userData.UserName;
                mailObject.ToEmail = userData.ContactEmail;
                mailObject.ValidateCode = str_code;
                mailObject.Password = userData.Password;
                mailObject.ReturnUrl = $"{ActionService.HttpHost}/User/ResetPasswordValidate/{str_code}";

                str_message = sendEmail.UserResetPassword(mailObject);
                if (string.IsNullOrEmpty(str_message))
                {
                    str_message = "您重設密碼的要求已受理，請記得收信完成重設密碼的流程!!!";
                }
            }

            //3.登出使用者
            SessionService.IsLogin = false;
            SessionService.UserNo = "";
            SessionService.UserName = "";

            //顯示註冊訊息
            SessionService.StringValue1 = "WebLogin";
            SessionService.MessageText = str_message;
            return RedirectToAction("Index", "Message", new { area = "" });
        }

        [HttpGet]
        [AllowAnonymous()]
        public ActionResult ResetPasswordValidate(string id)
        {
            using var user = new z_sqlUsers();
            //更新使用者狀態為已審核
            string str_message = user.ResetPasswordConfirm(id);
            //顯示重設密碼訊息
            SessionService.StringValue1 = "WebLogin";
            SessionService.MessageText = str_message;
            return RedirectToAction("Index", "Message", new { area = "" });
        }


        [HttpGet]
        [Login()]
        public ActionResult Upload()
        {
            ActionService.SetActionData("上傳照片", "", "我的帳號");
            return View();
        }

        [HttpPost]
        [Login()]
        public ActionResult Upload(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // 取得目前專案資料夾目錄路徑
                string FileNameOnServer = Directory.GetCurrentDirectory();
                // 專案路徑加入要存入的資料夾路徑
                FileNameOnServer += "\\wwwroot\\images\\users\\";
                // 以使用者名稱.jpg 存入
                FileNameOnServer += $"{SessionService.UserNo}.jpg";
                // 刪除已存在檔案
                if (System.IO.File.Exists(FileNameOnServer))
                    System.IO.File.Delete(FileNameOnServer);
                // 建立一個串流物件
                using var stream = System.IO.File.Create(FileNameOnServer);
                // 將檔案寫入到此串流物件中
                file.CopyTo(stream);
            }
            return RedirectToAction("Index", "Profile", new { area = "" });
        }


    }
}
