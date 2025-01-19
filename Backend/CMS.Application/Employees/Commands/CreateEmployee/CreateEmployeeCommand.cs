using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Employees.Commands.CreateEmployee
{

    public record CreateEmployeeResult(string Id);

    public record CreateEmployeeCommand(EmployeeDto Employee) : ICommand<CreateEmployeeResult>;


    public class CreateEmployeeCommandValidator: AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator() {
            RuleFor(x => x.Employee.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Employee.EmailAddress).NotEmpty().WithMessage("Email Address is required.");
            RuleFor(x => x.Employee.PhoneNumber).NotEmpty().WithMessage("Phone Number is required.");
        }
    }
}
