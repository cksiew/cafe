using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.CommonLib.CQRS
{
    public interface ICommandHandler<in TCommand>: ICommandHandler<ICommand, Unit> 
        where TCommand: ICommand<Unit>
    {

    }

    public interface ICommandHandler<in TCommand, TResponse>:IRequestHandler<TCommand,TResponse>
        where TCommand: ICommand<TResponse>
        where TResponse: notnull
    {

    }
}
