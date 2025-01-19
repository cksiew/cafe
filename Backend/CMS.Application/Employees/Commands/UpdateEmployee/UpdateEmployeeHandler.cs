using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeHandler(IApplicationDbContext dbContext) : ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeResult>
    {
        public async Task<UpdateEmployeeResult> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeDto = request.Employee;
            if (employeeDto.Id == null)
            {
                throw new ArgumentNullException(nameof(employeeDto.Id), "Employee ID cannot be null");
            }
            var employeeId = EmployeeId.Of(employeeDto.Id);
            var employee = await dbContext.Employees
                .FindAsync([employeeId], cancellationToken: cancellationToken) ?? throw new NotFoundException(nameof(Employee), employeeId.Value);
            employee.Update(
                employeeDto.Name,
                EmailAddress.Of(employeeDto.EmailAddress),
                PhoneNumber.Of(employeeDto.PhoneNumber),
                employeeDto.Gender
                );

            dbContext.Employees.Update(employee);
            int result = await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateEmployeeResult(result == 1);
        }
    }
}
