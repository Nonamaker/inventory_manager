namespace app.Models;

public class InventoryItemDTO
{
    public long? InventoryId { get; set; }
    public Inventory? Inventory { get; set; } = null;
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}