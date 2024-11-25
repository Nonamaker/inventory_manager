using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreWebAPI8.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class InventoryItemsController: ControllerBase
    {
        private readonly AppDbContext _context;
        public InventoryItemsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryItemDTO>>> GetInventoryItems()
        {
            if (_context.InventoryItems == null)
            {
                return NotFound();
            }
            return await _context.InventoryItems
                .Select(x => InventoryItemToDTO(x))
                .ToListAsync();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<InventoryItemDTO>> PostInventoryItem(InventoryItemDTO inventoryItemDto)
        {
            var inventoryItem = new InventoryItem
            {
                Name = inventoryItemDto.Name,
                Description = inventoryItemDto.Description,
                InventoryId = inventoryItemDto.InventoryId
            };
            _context.InventoryItems.Add(inventoryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetInventoryItem),
                new { id = inventoryItem.Id },
                InventoryItemToDTO(inventoryItem)
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItemDTO>> GetInventoryItem(long id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            return InventoryItemToDTO(inventoryItem);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutInventoryItem(long id, InventoryItemDTO inventoryItemDto)
        {
            if (id != inventoryItemDto.Id)
            {
                return BadRequest();
            }

            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            inventoryItem.Name = inventoryItemDto.Name;
            inventoryItem.Description = inventoryItemDto.Description;
            inventoryItem.InventoryId = inventoryItemDto.InventoryId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!InventoryItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteInventoryItem(long id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }

            _context.InventoryItems.Remove(inventoryItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryItemExists(long id)
        {
            return _context.InventoryItems.Any(e => e.Id == id);
        }

        private static InventoryItemDTO InventoryItemToDTO(InventoryItem inventoryItem) =>
            new InventoryItemDTO
            {
                Id = inventoryItem.Id,
                Name = inventoryItem.Name,
                Description = inventoryItem.Description,
                InventoryId = inventoryItem.InventoryId
            };

    }

}