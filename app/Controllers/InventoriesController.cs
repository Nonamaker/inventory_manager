using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AspNetCoreWebAPI8.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController: ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public InventoriesController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetInventories()
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }
            // Only show inventories owned by the user
            IdentityUser currentUser = (await _userManager.GetUserAsync(HttpContext.User))!;
            return await _context.Inventories
                .Where(x => x.Owner == currentUser)
                .Select(x => InventoryToDTO(x))
                .ToListAsync();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<InventoryDTO>> PostInventory(InventoryDTO inventoryDto)
        {

            IdentityUser currentUser = (await _userManager.GetUserAsync(HttpContext.User))!;

            var inventory = new Inventory
            {
                Name = inventoryDto.Name,
                Description = inventoryDto.Description,
                OwnerId = currentUser.Id,            
                Owner = currentUser
            };
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetInventory),
                new { id = inventory.Id },
                InventoryToDTO(inventory)
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDTO>> GetInventory(long id)
        {
            IdentityUser currentUser = (await _userManager.GetUserAsync(HttpContext.User))!;

            var inventory = await _context.Inventories.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }
            // Users can only acces inventories they own
            if (inventory.Owner != currentUser )
            {
                return NotFound();
            }

            return InventoryToDTO(inventory);
        }


        [HttpGet("{id}/contents")]
        public async Task<ActionResult<IEnumerable<InventoryItemDTO>>> GetInventoryContents(long id)
        {
            IdentityUser currentUser = (await _userManager.GetUserAsync(HttpContext.User))!;

            var inventory = await _context.Inventories.FindAsync(id);

            if (inventory == null)
            {
                return NotFound();
            }
            // Users can only acces inventories they own
            if (inventory.Owner != currentUser )
            {
                return NotFound();
            }

            return await _context.InventoryItems
                .Where(x => x.InventoryId == id)
                .Select(x => new InventoryItemDTO(x))
                .ToListAsync();
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutInventory(long id, InventoryDTO inventoryDto)
        {
            // TODO Make sure user owns the inventory
            if (id != inventoryDto.Id)
            {
                return BadRequest();
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            inventory.Name = inventoryDto.Name;
            inventory.Description = inventoryDto.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!InventoryExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteInventory(long id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(long id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }

        private static InventoryDTO InventoryToDTO(Inventory inventory) =>
            new InventoryDTO
            {
                Id = inventory.Id,
                Name = inventory.Name,
                Description = inventory.Description
            };

    }

}