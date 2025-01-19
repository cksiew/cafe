using CMS.CommonLib.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Employees.Queries.GetEmployeesByCafe
{
    public record GetEmployeesByCafeQuery(string CafeId, PaginationRequest PaginationRequest) : IQuery<GetEmployeesByCafeResult>;

    public record GetEmployeesByCafeResult(PaginatedResult<EmployeeDto> Employees);
}
