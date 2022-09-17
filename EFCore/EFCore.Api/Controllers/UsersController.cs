using EFCore.Core.Data;
using EFCore.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public UsersController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        [HttpPost]
        public IActionResult CreateUser()
        {
            var user = new User("John");
            user.Tags.Add("tag1");
            user.Tags.Add("tag2");

            _userDbContext.Add(user);
            _userDbContext.SaveChanges();

            return Ok(new
            {
                UserId = user.Id
            });
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUser([FromRoute] long id)
        {
            var user = _userDbContext.Users.Find(id);
            

            return Ok(new
            {
                UserId = user?.Id,
                Name = user?.Name,
                Tags = user?.Tags
            });
        }


        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateUser([FromRoute] long id)
        {
            var user = _userDbContext.Users.Find(id);

            var newTags = new List<string>();
            newTags.AddRange(user.Tags);

            newTags.Add("updated tag1");
            newTags.Add("updated tag2");

            user.Tags = newTags;

            _userDbContext.SaveChanges();

            return Ok(new
            {
                UserId = user?.Id,
                Name = user?.Name,
                Tags = user?.Tags
            });
        }


    }
}
