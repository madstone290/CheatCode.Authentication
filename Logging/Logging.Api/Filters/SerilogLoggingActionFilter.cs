using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace Logging.Api.Filters
{
    public class SerilogLoggingActionFilter : IActionFilter
    {
        private readonly IDiagnosticContext _diagnosticContext;
        public SerilogLoggingActionFilter(IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // 진단데이터를 추가로 등록할 수 있다.
            _diagnosticContext.Set("RouteData", context.ActionDescriptor.RouteValues);
            _diagnosticContext.Set("Action", context.ActionDescriptor.RouteValues["action"]);
            _diagnosticContext.Set("Controller", context.ActionDescriptor.RouteValues["controller"]);
            //_diagnosticContext.Set("ActionId", context.ActionDescriptor.Id);
            //_diagnosticContext.Set("ValidationState", context.ModelState.IsValid);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
