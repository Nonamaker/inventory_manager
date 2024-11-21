using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace app.Models;

public class AppDbContext : IdentityDbContext<IdentityUser>
{

    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InventoryItem> InventoryItems { get; set; }
}