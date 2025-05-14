using Drool_FrontToBack.Models;
using Microsoft.EntityFrameworkCore;

namespace Drool_FrontToBack.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }



    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

}
