using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using shopping.Models;

namespace shopping.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [Login()]
    public IActionResult Open(string id)
    {
        using var prg = new z_sqlPrograms();
        var data = prg.GetData(id);
        if (data == null)
        {
            return RedirectToAction("Index", "Home", new { area = SessionService.RoleNo });
        }
        SessionService.SetProgramInfo(data.PrgNo, data.PrgName, data.IsPageSize, data.IsSearch, data.PageSize);
        return RedirectToAction(data.ActionName, data.ControllerName, new { area = data.AreaName });
    }
}
