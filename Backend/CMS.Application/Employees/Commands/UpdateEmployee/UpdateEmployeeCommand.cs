using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Employees.Commands.UpdateEmployee
{
    public record UpdateEmployeeResult(bool IsSuccess);

    public record UpdateEmployeeCommand(EmployeeDto Employee) : ICommand<UpdateEmployeeResult>;


    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator()
        {
            RuleFor(x => x.Employee.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Employee.EmailAddress).NotEmpty().WithMessage("Email Address is required.");
            RuleFor(x => x.Employee.PhoneNumber).NotEmpty().WithMessage("Phone Number is required.");
        }
    }
}
