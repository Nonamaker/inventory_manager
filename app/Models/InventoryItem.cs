using System;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;


namespace app.Models;

[Table("InventoryItems")]

public class InventoryItem

{

    [Key]

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

    public long Id { get; set; }


    [Required(ErrorMessage = "Item name cannot be empty.")]

    public string Name { get; set; } = string.Empty;


    [Required(ErrorMessage = "Item description cannot be empty.")]

    public string Description { get; set; } = string.Empty;


}