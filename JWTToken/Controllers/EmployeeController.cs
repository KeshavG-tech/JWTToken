using JWTToken.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("allprodt")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
                return await _context.Products.ToListAsync();
            
        }

    }
}
