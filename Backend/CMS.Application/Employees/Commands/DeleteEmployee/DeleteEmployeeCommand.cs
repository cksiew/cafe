using FluentValidation;
using FluentValidation.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Employees.Commands.DeleteEmployee
{
    public record DeleteEmployeeCommand(string EmployeeId) : ICommand<DeleteEmployeeResult>;

    public record DeleteEmployeeResult(bool IsSuccess);

    public class DeleteEmployeeCommandValidator: AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.EmployeeId).NotNull().WithMessage("Employee Id is required.");
        }
    }
   
}
