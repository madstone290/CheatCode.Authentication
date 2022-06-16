using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4B.ApiOne.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = new object[]
            {
                new { Name = "John", Age = 34},
                new { Name = "Alice", Age = 44},
                new { Name = "Bruce", Age = 54},
                new { Name = "Joe", Age = 24},
            };
            return Ok(users);
        }

    }
}
