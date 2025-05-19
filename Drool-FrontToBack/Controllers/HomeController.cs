using Drool_FrontToBack.Contexts;
using Drool_FrontToBack.ViewModels;
using Drool_FrontToBack.ViewModels.CategoryViewModels;
using Drool_FrontToBack.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Drool_FrontToBack.Controllers;

public class HomeController(AppDbContext _context) : Controller
{


    public async Task<IActionResult> Index()
    {

        var products = await _context.Products.Select(x => new ProductGetVM()
        {
            Name = x.Name,
            ImagePath = x.ImagePath,
            Id = x.Id,
            Description = x.Description,
            Category = new()
            {
                Id = x.Category.Id,
                Name = x.Category.Name,
            }
        }).ToListAsync();

        var categories = await _context.Categories.Select(x => new CategoryGetVM() { Id = x.Id, Name = x.Name }).ToListAsync();

        HomeVM vm = new()
        {
            Products = products,
            Categories=categories
        };

        return View(vm);
    }

}
