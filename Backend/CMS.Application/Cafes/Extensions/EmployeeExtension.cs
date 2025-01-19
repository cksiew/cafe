using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Cafes.Extensions
{
    public static class EmployeeExtensions
    {
        public static IEnumerable<EmployeeDto> ToEmployeeDtoList(this IEnumerable<Employee> employees) {

            return employees.Select(e =>
            {
                return DtoFromEmployee(e);
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
                DaysWork: employee.DaysOfWork,
                CafeId: employee.Cafe.Id.Value.ToString(),
                CafeName: employee.Cafe.Name);
        }
    }
}
