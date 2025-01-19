namespace CMS.Application.Employees.Commands.CreateEmployee
{
    public class CreateEmployeeHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateEmployeeCommand, CreateEmployeeResult>
    {
        public async Task<CreateEmployeeResult> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = CreateNewEmployee(command.Employee);
            dbContext.Employees.Add(employee);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateEmployeeResult(employee.Id.Value);
        }

        private static Employee CreateNewEmployee(EmployeeDto employeeDTO)
        {

            var newEmployee = Employee.Create(
                name: employeeDTO.Name,
                emailAddress: EmailAddress.Of(employeeDTO.EmailAddress),
                phoneNumber: PhoneNumber.Of(employeeDTO.PhoneNumber),
                gender: employeeDTO.Gender,
                cafeId: CafeId.Of(Guid.Parse(employeeDTO.CafeId)));

            return newEmployee;
        }
    }
}
