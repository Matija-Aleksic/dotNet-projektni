using Microsoft.AspNetCore.Mvc;
using dotNet_projektni.Data;
using dotNet_projektni.Tests;

namespace dotNet_projektni.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("crud")]
        public async Task<IActionResult> RunCrudTests()
        {
            var tests = new CrudTests(_context);
            bool success = await tests.RunAllTests();
            
            if (success)
            {
                return Ok(new { message = "All CRUD tests passed successfully!" });
            }
            else
            {
                return BadRequest(new { message = "Some tests failed. Check logs for details." });
            }
        }
    }
}

