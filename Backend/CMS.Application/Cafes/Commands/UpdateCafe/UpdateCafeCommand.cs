using CMS.Application.Dtos;
using CMS.CommonLib.CQRS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CMS.Application.Cafes.Commands.UpdateCafe
{
    public record UpdateCafeResult(bool IsSuccess);
    public record UpdateCafeCommand(CafeCreateUpdateDto Cafe): ICommand<UpdateCafeResult>;

    public class UpdateCommandValidator : AbstractValidator<UpdateCafeCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Cafe.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Cafe.Location).NotEmpty().WithMessage("Location is required.");
            RuleFor(x => x.Cafe.Description).NotEmpty().WithMessage("Description is required.");
        }
    }
}
