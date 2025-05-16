using Drool_FrontToBack.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Drool_FrontToBack.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }



    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

}
