using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Employees.Extensions
{
    public static class EmployeeExtensions
    {
        public static IEnumerable<EmployeeDto> ToEmployeeDtoList(this IEnumerable<Employee> employees)
        {
            return employees.Select(c =>
            {
                return DtoFromEmployee(c);
            });
        }

        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return DtoFromEmployee(employee);
        }

        private static EmployeeDto DtoFromEmployee(Employee employee)
        {
            return new EmployeeDto(
                Id: employee.Id.Value,
                Name: employee.Name,
                Gender: employee.Gender,
                EmailAddress: employee.EmailAddress.Value,
                PhoneNumber: employee.PhoneNumber.Value,
                CafeId: employee.CafeId!.Value.ToString(),
                DaysWork: employee.DaysOfWork,
                CafeName: employee.Cafe.Name
                );
        }
    }
}
