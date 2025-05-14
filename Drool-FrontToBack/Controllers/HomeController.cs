using Microsoft.AspNetCore.Mvc;

namespace Drool_FrontToBack.Controllers;

public class HomeController : Controller
{
    

    public IActionResult Index()
    {
        return View();
    }

}
