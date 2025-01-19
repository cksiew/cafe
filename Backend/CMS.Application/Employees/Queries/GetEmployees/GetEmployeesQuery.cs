using CMS.CommonLib.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Employees.Queries.GetEmployees
{
    public record GetEmployeesQuery(PaginationRequest PaginationRequest):IQuery<GetEmployeesResult>;

    public record GetEmployeesResult(PaginatedResult<EmployeeDto> Employees);
}
