using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cqrs.Application
{
    /// <summary>
    /// 요청에 필요한 기본정보를 가진다
    /// </summary>
    public class Command<TResult> : ICommand<TResult>
    {
        public object OwnerId { get; set; }
    }
}
