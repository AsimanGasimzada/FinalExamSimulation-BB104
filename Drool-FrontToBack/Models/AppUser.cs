using Microsoft.AspNetCore.Identity;

namespace Drool_FrontToBack.Models;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; }
}
