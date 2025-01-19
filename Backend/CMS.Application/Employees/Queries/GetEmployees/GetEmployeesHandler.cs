using CMS.Application.Cafes.Queries.GetCafes;
using CMS.Application.Employees.Extensions;
using CMS.CommonLib.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CMS.Application.Employees.Queries.GetEmployees
{
    public class GetEmployeesHandler(IApplicationDbContext dbContext) : IQueryHandler<GetEmployeesQuery, GetEmployeesResult>
    {
        public async Task<GetEmployeesResult> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Employees.LongCountAsync(cancellationToken);

            var employees = await dbContext.Employees
                .Include(e => e.Cafe)
                .AsNoTracking()
                .OrderByDescending(e => e.CreatedAt)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetEmployeesResult(
                new PaginatedResult<EmployeeDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    employees.ToEmployeeDtoList()
                )
                );
        }
    }

}
