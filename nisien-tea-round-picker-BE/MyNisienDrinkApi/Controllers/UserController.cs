using Microsoft.AspNetCore.Mvc;
using MyNisienDrinkApi.Models; 
using MyNisienDrinkApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MyNisienDrinkApi.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
           
            user.Id = Guid.NewGuid().ToString();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _context.Users.Include(u => u.DrinkOrders).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();
            return user;
        }
    }
}
