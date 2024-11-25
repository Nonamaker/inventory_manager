namespace app.Models;

public class InventoryItemDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long? InventoryId { get; set; }

    public InventoryItemDTO() {
    }

    public InventoryItemDTO(InventoryItem inventoryItem) {
        Id = inventoryItem.Id;
        Name = inventoryItem.Name;
        Description = inventoryItem.Description;
        InventoryId = inventoryItem.InventoryId;
    }
}