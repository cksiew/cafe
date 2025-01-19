using CMS.Application.Employees.Extensions;
using CMS.CommonLib.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CMS.Application.Employees.Queries.GetEmployeesByCafe
{
    public class GetEmployeesByCafeHandler(IApplicationDbContext dbContext) : IQueryHandler<GetEmployeesByCafeQuery, GetEmployeesByCafeResult>
    {
        public async Task<GetEmployeesByCafeResult> Handle(GetEmployeesByCafeQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Employees.LongCountAsync(e => e.CafeId! == CafeId.Of(Guid.Parse(query.CafeId)), cancellationToken);

            var employees = await dbContext.Employees
                .Include(e => e.Cafe)
                .AsNoTracking()
                .Where(e => e.CafeId! == CafeId.Of(Guid.Parse(query.CafeId)))
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync(cancellationToken);

            return new GetEmployeesByCafeResult(
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
