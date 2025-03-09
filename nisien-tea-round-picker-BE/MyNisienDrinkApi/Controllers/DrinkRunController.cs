using Microsoft.AspNetCore.Mvc;
using MyNisienDrinkApi.Models; 
using MyNisienDrinkApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MyNisienDrinkApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class DrinkRunController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DrinkRunController(AppDbContext context)
        {
            _context = context;
        }

    [HttpPost("StartDrinkRun")]
public async Task<ActionResult<DrinkRun>> StartDrinkRun([FromBody] DrinkRunPayload payload)
{
    if (payload == null || payload.Participants == null || !payload.Participants.Any())
    {
        return BadRequest("No participants provided.");
    }

    var randomIndex = new Random().Next(payload.Participants.Count);
    var selectedParticipant = payload.Participants[randomIndex];
    var drinkMaker = await _context.Users.FirstOrDefaultAsync(u => u.Id == selectedParticipant.UserId);

    if (drinkMaker == null)
    {
        return NotFound("Selected drink maker not found.");
    }

    var orders = await _context.DrinkOrders
        .Where(o => payload.Participants.Select(p => p.DrinkOrderId).Contains(o.Id)) // Make sure DrinkOrderId is used
        .ToListAsync();

    var drinkRun = new DrinkRun
    {
        Id = Guid.NewGuid().ToString(),
        DrinkMaker = drinkMaker,
        Orders = orders,
    };

    _context.DrinkRuns.Add(drinkRun);
    await _context.SaveChangesAsync();

    return Ok(drinkRun);
}

    }
}
