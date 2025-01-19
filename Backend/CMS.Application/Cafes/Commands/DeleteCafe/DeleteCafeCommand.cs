using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Cafes.Commands.DeleteCafe
{
    public record DeleteCafeCommand(Guid CafeId): ICommand<DeleteCafeResult>;

    public record DeleteCafeResult(bool IsSuccess);

    public class DeleteCafeCommandValidator: AbstractValidator<DeleteCafeCommand>
    {
        public DeleteCafeCommandValidator()
        {
            RuleFor(x => x.CafeId).NotNull().WithMessage("Cafe Id is required.");
        }
    }
}
