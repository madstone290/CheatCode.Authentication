using EFCore.Core.Data;
using EFCore.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Api.Controllers
{
    [ApiController]
    [Route("Data")]
    public class DataController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public DataController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }




        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            var user = new User("John with " + DateTime.Now.ToLongDateString());
            _userDbContext.Add(user);

            _userDbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("v1")]
        public IActionResult Method1()
        {
            var user = _userDbContext.Users.FirstOrDefault();
            if (user == null)
                return NoContent();
            
            user.Name = Guid.NewGuid().ToString();
            user.Color = Guid.NewGuid().ToString();

            var state = _userDbContext.Entry(user).State;

            _userDbContext.Update(user);


            _userDbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("v2")]
        public IActionResult Method2()
        {
            var user = _userDbContext.Users.FirstOrDefault();
            if (user == null)
                return NoContent();

            user.Name = Guid.NewGuid().ToString();
            user.Color = Guid.NewGuid().ToString();


            var state = _userDbContext.Entry(user).State;
            if (state == Microsoft.EntityFrameworkCore.EntityState.Detached)
                _userDbContext.Update(user);
            else if(state == Microsoft.EntityFrameworkCore.EntityState.Added)
                _userDbContext.Add(user);
            
            _userDbContext.SaveChanges();
            return Ok();
        }

    }
}
