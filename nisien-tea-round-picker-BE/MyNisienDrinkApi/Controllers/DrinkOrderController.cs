using Microsoft.AspNetCore.Mvc;
using MyNisienDrinkApi.Models;
using MyNisienDrinkApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MyNisienDrinkApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DrinkOrderController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DrinkOrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<DrinkOrder>> CreateDrinkOrder([FromBody] DrinkOrder drinkOrder)
        {
            var user = await _context.Users.FindAsync(drinkOrder.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            _context.DrinkOrders.Add(drinkOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDrinkOrder), new { id = drinkOrder.Id }, drinkOrder);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DrinkOrder>> GetDrinkOrder(string id)
        {
            var drinkOrder = await _context.DrinkOrders
                .Include(doi => doi.DrinkRunParticipants) // Ensure this property exists in your model
                .FirstOrDefaultAsync(doi => doi.Id == id);

            if (drinkOrder == null)
            {
                return NotFound("Drink order not found.");
            }

            return Ok(drinkOrder);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetUserDrinkOrders(string userId)
        {
            var user = await _context.Users
                .Include(u => u.DrinkOrders)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user.DrinkOrders);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDrinkOrders()
        {
            var drinkOrders = await _context.DrinkOrders.ToListAsync();
            return Ok(drinkOrders);
        }
    }
}
