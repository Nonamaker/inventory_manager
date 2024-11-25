using System;

using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;


namespace app.Models;

[Table("Inventories")]

public class Inventory

{

    [Key]
    public long Id { get; set; }

    [Required(ErrorMessage = "Inventory name cannot be empty.")]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? OwnerId { get; set; }
    [ForeignKey("OwnerId")]
    public IdentityUser? Owner { get; set; } = null;

}