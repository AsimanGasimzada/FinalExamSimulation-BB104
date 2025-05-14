using Microsoft.AspNetCore.Mvc;

namespace Drool_FrontToBack.Areas.Admin.Controllers;
[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
