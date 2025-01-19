using CMS.CommonLib.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.Application.Cafes.Extensions;

namespace CMS.Application.Cafes.Queries.GetCafes
{
    public class GetCafesHandler(IApplicationDbContext dbContext) : IQueryHandler<GetCafesQuery, GetCafesResult>
    {
        public async Task<GetCafesResult> Handle(GetCafesQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Cafes.LongCountAsync(cancellationToken);

            var cafes = await dbContext.Cafes
                .Include(c => c.Employees)
                .AsNoTracking()
                .OrderByDescending(o => o.Employees.Count)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetCafesResult(
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
