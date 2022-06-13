using Cqrs.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Application.Cars.Commands
{
    public class CreateCarCommand : Command<Car>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CreateCarCommandHandler : ICommandHandler<CreateCarCommand, Car>
    {
        public Task<Car> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Car car = new Car() { Name = request.Name };

            return Task.FromResult(car);
        }
    }
}
