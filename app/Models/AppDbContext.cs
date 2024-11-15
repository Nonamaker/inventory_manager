using Microsoft.EntityFrameworkCore;

namespace app.Models;

public class AppDbContext : DbContext
{

    public AppDbContext() { }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<InventoryItem> InventoryItems { get; set; }
}