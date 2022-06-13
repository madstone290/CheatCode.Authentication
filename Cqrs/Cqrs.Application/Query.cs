using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Application
{
    public class Query<TResult> : IQuery<TResult>
    {
        public object OwnerId { get; set; }
    }
}
