using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityServer4A.MvcApp.Filters
{
    public class LogFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("excuted!!!: " + context.HttpContext.Request.Path);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("excuting...: " + context.HttpContext.Request.Path);
        }
    }
}
