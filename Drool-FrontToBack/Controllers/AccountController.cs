using Drool_FrontToBack.Models;
using Drool_FrontToBack.ViewModels.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Drool_FrontToBack.Controllers;
public class AccountController(RoleManager<IdentityRole> _roleManager, UserManager<AppUser> _userManager, SignInManager<AppUser> _signInManager) : Controller
{

    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);



        AppUser appUser = new()
        {
            UserName = vm.Username,
            Email = vm.Email,
            Fullname = vm.Fullname
        };


        var result = await _userManager.CreateAsync(appUser, vm.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(vm);
        }


        await _userManager.AddToRoleAsync(appUser, "Member");


        await _signInManager.SignInAsync(appUser, isPersistent: false);

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }


    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if (!ModelState.IsValid)
            return View(vm);


        var user = await _userManager.FindByEmailAsync(vm.Email);

        if (user is null)
        {
            ModelState.AddModelError("", "Parol ve ya email sehvdir");
            return View(vm);
        }


        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, isPersistent: false, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Parol ve ya email sehvdir");
            return View(vm);
        }

        return RedirectToAction("Index", "Home");
    }

    //public async Task<IActionResult> CreateRoles()
    //{

    //    await _roleManager.CreateAsync(new() { Name = "Admin" });
    //    await _roleManager.CreateAsync(new() { Name = "Member" });
    //    await _roleManager.CreateAsync(new() { Name = "Nazarata nazarat" });


    //    return Ok("Rollar yarandi");
    //}

    public async Task<IActionResult> CreateAdmin()
    {
        AppUser admin = new()
        {
            UserName = "Admin",
            Fullname = "Admin",
            Email = "admin@gmail.com"
        };

        await _userManager.CreateAsync(admin, "admin1234");
        await _userManager.AddToRoleAsync(admin, "Admin");

        return Ok("Created admin");
    }
}
