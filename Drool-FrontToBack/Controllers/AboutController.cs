using Microsoft.AspNetCore.Mvc;

namespace Drool_FrontToBack.Controllers;
public class AboutController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
