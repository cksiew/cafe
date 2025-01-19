using CMS.Application.Cafes.Extensions;
using CMS.CommonLib.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Cafes.Queries.GetCafesByLocation
{
    public class GetCafesByLocationHandler(IApplicationDbContext dbContext) : IQueryHandler<GetCafesByLocationQuery, GetCafesByLocationResult>
    {
        public async Task<GetCafesByLocationResult> Handle(GetCafesByLocationQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Cafes.LongCountAsync(c => c.Location == query.Location, cancellationToken);

            var cafes = await dbContext.Cafes
                .Include(c => c.Employees)
                .AsNoTracking()
                .Where(c => c.Location == query.Location)
                .OrderByDescending(o => o.Employees.Count)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetCafesByLocationResult(
                new PaginatedResult<CafeDto>(
                    pageIndex,
                    pageSize,
                    totalCount,
                    cafes.ToCafeDtoList()
                )
                );
        }
    }
}
