using EFCore.Core.Data;
using Microsoft.AspNetCore.Mvc;

namespace EFCore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaveChangesOrderController : ControllerBase
    {
        private readonly UserDbContext _userDbContext;

        public SaveChangesOrderController(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        [HttpGet]
        [Route("SaveChanges")]
        public IActionResult SaveChanges()
        {
            _userDbContext.SaveChanges();

            return Ok(_userDbContext.Watcher.JoinWithNewLine());
        }

        [HttpGet]
        [Route("SaveChangesWithParam")]
        public IActionResult SaveChangesWithParam()
        {
            _userDbContext.SaveChanges(false);
            var whenTrue = _userDbContext.Watcher;
            _userDbContext.Watcher.Clear();
            _userDbContext.SaveChanges(true);
            var whenFalse = _userDbContext.Watcher;
            return Ok(new { True = whenTrue.JoinWithNewLine(), False = whenFalse.JoinWithNewLine() });
        }

        [HttpGet]
        [Route("SaveChangesAsync")]
        public IActionResult SaveChangesAsync()
        {
            _userDbContext.SaveChangesAsync();

            return Ok(_userDbContext.Watcher.JoinWithNewLine());
        }

        [HttpGet]
        [Route("SaveChangesAsyncWithParam")]
        public IActionResult SaveChangesAsyncWithParam()
        {
            _userDbContext.SaveChangesAsync(false);
            var whenTrue = _userDbContext.Watcher;
            _userDbContext.Watcher.Clear();
            _userDbContext.SaveChangesAsync(true);
            var whenFalse = _userDbContext.Watcher;
            return Ok(new { True = whenTrue.JoinWithNewLine(), False = whenFalse.JoinWithNewLine() });
        }



    }
}
