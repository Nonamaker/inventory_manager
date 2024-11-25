namespace app.Models;
using Microsoft.AspNetCore.Identity;

public class InventoryDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IdentityUser? Owner { get; set; } = null;
}