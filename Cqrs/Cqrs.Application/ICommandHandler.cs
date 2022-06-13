using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Application
{
    public interface ICommandHandler<TCommand, TResult> :  IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>
    {
    }
}
