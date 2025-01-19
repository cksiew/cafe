using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Cafes.Commands.CreateCafe
{
    public record CreateCafeResult(Guid Id);

    public record CreateCafeCommand(CafeCreateUpdateDto Cafe):ICommand<CreateCafeResult>;


    public class CreateCafeCommandValidator : AbstractValidator<CreateCafeCommand>
    {
        public CreateCafeCommandValidator()
        {
            RuleFor(x => x.Cafe.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Cafe.Location).NotEmpty().WithMessage("Location is required.");
            RuleFor(x => x.Cafe.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(x => x.Cafe.LogoFile).NotEmpty().WithMessage("LogoFile is required.");
        }
    }
}
