using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using app.Models;

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
        public async Task<ActionResult<IEnumerable<InventoryItem>>> GetInventoryItems()
        {
            if (_context.InventoryItems == null)
            {
                return NotFound();
            }
            return await _context.InventoryItems.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItem>> GetInventoryItem(long id)
        {
            var inventoryItem = await _context.InventoryItems.FindAsync(id);

            if (inventoryItem == null)
            {
                return NotFound();
            }

            return inventoryItem;
        }

    }

}