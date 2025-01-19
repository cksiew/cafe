using CMS.Application.Employees.Commands.DeleteEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CMS.Application.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>
    {
        public async Task<DeleteEmployeeResult> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employeeId = EmployeeId.Of(command.EmployeeId);

            var employee = await dbContext.Employees.FindAsync([employeeId], cancellationToken) ?? throw new NotFoundException(nameof(Employee), command.EmployeeId);
            dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteEmployeeResult(true);
        }
    }
}
