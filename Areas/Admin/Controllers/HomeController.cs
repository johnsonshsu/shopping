using Microsoft.AspNetCore.Mvc;

namespace shopping.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        [HttpGet]
        [Login(RoleList = "User,Mis,Member")]
        public IActionResult Init()
        {
            SessionService.SetPrgInit();
            return RedirectToAction("Index", ActionService.Controller, new { area = ActionService.Area });
        }

        [Area("Admin")]
        [HttpGet]
        [Login(RoleList = "User,Mis,Member")]
        public IActionResult Index()
        {
            SessionService.SetProgramInfo("", "儀表板", false, false, 0);
            SessionService.SetActionInfo(enAction.Dashboard, enCardSize.Max);
            return View();
        }
    }
}
