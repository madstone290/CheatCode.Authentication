using Cqrs.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Application.Cars.Queries
{
    public class GetAllCarsQuery : Query<IEnumerable<Car>>
    {

    }

    public class GetAllCarsQueryHandler : IQueryHandler<GetAllCarsQuery, IEnumerable<Car>>
    {
        public Task<IEnumerable<Car>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var result = new List<Car>()
            {
                new Car(){ Name = $"Bmw 520i owner: {request.OwnerId}"},
                new Car(){ Name = $"Kia Sorrento owner: {request.OwnerId}"},
            };
            return Task.FromResult((IEnumerable<Car>)result);
        }
    }
}
