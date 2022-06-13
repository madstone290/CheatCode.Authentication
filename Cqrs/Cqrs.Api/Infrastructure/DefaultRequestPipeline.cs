using Cqrs.Application;
using Cqrs.Application.Models;
using MediatR;
using System.Security.Claims;

namespace Cqrs.Api.Infrastructure
{
    public class DefaultRequestPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly HttpContext _httpContext;

        public DefaultRequestPipeline(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext!;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // mediator 파이프라인을 구성한다

            if(request is Command<TResponse> command)
            {
                command.OwnerId = "Command owner";
            }
            if(request is Query<TResponse> query)
            {
                query.OwnerId = "Query owner";
            }

            // before action

            var response = await next();

            // after action

            if(response is Car car)
            {
                car.Name += " Checked";
            }

            return response;
        }
    }
}
