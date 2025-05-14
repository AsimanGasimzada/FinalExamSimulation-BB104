using Drool_FrontToBack.Contexts;
using Drool_FrontToBack.Models;
using Drool_FrontToBack.ViewModels.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Drool_FrontToBack.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController(AppDbContext _context, IWebHostEnvironment _environment) : Controller
{
    public async Task<IActionResult> Index()
    {

        var products = await _context.Products.Select(x => new ProductGetVM() { Id = x.Id, Name = x.Name, Description = x.Description, ImagePath = x.ImagePath }).ToListAsync();

        return View(products);
    }


    public async Task<IActionResult> Create()
    {
        var categories = await _context.Categories.ToListAsync();

        ViewBag.Categories = categories;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateVM vm)
    {

        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = categories;

        if (!ModelState.IsValid)
            return View(vm);

        if (vm.Image.Length > 2 * 1024 * 1024)
        {
            ModelState.AddModelError("Image", "Image 2 mb dan boyuk ola bilmez");
            return View(vm);
        }
        if (!vm.Image.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Image", "Yalniz sekil formatinda data daxil ede bilersiniz");
            return View(vm);
        }

        var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

        if (isExistCategory is false)
        {
            ModelState.AddModelError("CategoryId", "Bu id-li category yoxdur");
            return View(vm);
        }


        string filename = Guid.NewGuid().ToString() + vm.Image.FileName;  //askdjdsankjdsankjndsah+fish.png
        string path = Path.Combine(_environment.WebRootPath, "images", filename);

        using FileStream stream = new(path, FileMode.OpenOrCreate);

        await vm.Image.CopyToAsync(stream);

        Product product = new()
        {
            Name = vm.Name,
            Description = vm.Description,
            ImagePath = filename,
            CategoryId = vm.CategoryId
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Update(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (product is null)
            return NotFound();

        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = categories;

        ProductUpdateVM vm = new()
        {
            Id = id,
            Name = product.Name,
            Description = product.Description,
            CategoryId = product.CategoryId,

        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int id, ProductUpdateVM vm)
    {
        var categories = await _context.Categories.ToListAsync();
        ViewBag.Categories = categories;

        if (!ModelState.IsValid)
            return View(vm);

        var existProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

        if (existProduct is null)
            return BadRequest();

        if (vm.Image is not null)
        {
            if (vm.Image.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Image 2 mb dan boyuk ola bilmez");
                return View(vm);
            }
            if (!vm.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Yalniz sekil formatinda data daxil ede bilersiniz");
                return View(vm);
            }
        }

        var isExistCategory = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);

        if (isExistCategory is false)
        {
            ModelState.AddModelError("CategoryId", "Bu id-li category yoxdur");
            return View(vm);
        }


        existProduct.Name = vm.Name;
        existProduct.Description = vm.Description;
        existProduct.CategoryId = vm.CategoryId;

        if (vm.Image is not null)
        {
            string filename = Guid.NewGuid().ToString() + vm.Image.FileName;
            string path = Path.Combine(_environment.WebRootPath, "images", filename);
            using FileStream stream = new(path, FileMode.OpenOrCreate);
            await vm.Image.CopyToAsync(stream);


            if (System.IO.File.Exists(Path.Combine(_environment.WebRootPath, "images", existProduct.ImagePath)))
            {
                System.IO.File.Delete(Path.Combine(_environment.WebRootPath, "images", existProduct.ImagePath));
            }

            existProduct.ImagePath = filename;
        }

        _context.Products.Update(existProduct);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (product is null)
            return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();


        if (System.IO.File.Exists(Path.Combine(_environment.WebRootPath, "images", product.ImagePath)))
        {
            System.IO.File.Delete(Path.Combine(_environment.WebRootPath, "images", product.ImagePath));
        }

        return RedirectToAction("Index");
    }
}
