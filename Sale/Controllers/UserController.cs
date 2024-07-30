using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sale.Model;
using System.Threading.Tasks;

namespace Sale.Context
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _authcontext;

        public UserController(AppDbContext appDbContext)
        {
            _authcontext = appDbContext;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null || string.IsNullOrEmpty(userObj.Username) || string.IsNullOrEmpty(userObj.Password))
                return BadRequest(new { Message = "Username and Password are required" });

            var user = await _authcontext.Users
                .FirstOrDefaultAsync(x => x.Username == userObj.Username && x.Password == userObj.Password);

            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(new { Message = "Login Success" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            try
            {
                await _authcontext.Users.AddAsync(userObj);
                await _authcontext.SaveChangesAsync();
                return Ok(new { Message = "User Registered" });
            }
            catch (DbUpdateException ex)
            {
                // Log the inner exception details
                var innerException = ex.InnerException?.Message;
                return StatusCode(500, $"Internal server error: {innerException}");
            }
        }
    }
}
