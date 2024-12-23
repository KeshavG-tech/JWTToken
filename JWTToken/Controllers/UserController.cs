using JWTToken.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration configuration;

        public UserController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration; 
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(Login login)
        {
            var user = _context.Users.FirstOrDefault(x=>x.Email == login.Email && x.Password == login.Password);

            if(user !=null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("UserId", user.Id.ToString()),
                     new Claim("Email", user.Email.ToString()),
                     //Jb different role add krne ho tb
                    // new Claim(ClaimTypes.Role, user.Role) // Role ko claims mein add kar rahe hain
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var singin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(5),
                    signingCredentials: singin
                    );

                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token); 

                return Ok(new {Token = tokenValue , User = user});
                //return Ok(user);
            }

            return NoContent();
        }

        //For roles
        //[Authorize(Roles= "Admin")]
        //[Authorize(Roles = "Admin,Customer")]
        [Authorize]
        [HttpGet]
        [Route("allprodt")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();

        }

        [HttpGet]
        [Route("prodt/id")]
        public IActionResult GetProductbyid([FromQuery] int proId)
        {
            var ans = _context.Products.Where(x => x.Id == proId).ToList();
                return Ok(ans);
            
        }
    }
}
