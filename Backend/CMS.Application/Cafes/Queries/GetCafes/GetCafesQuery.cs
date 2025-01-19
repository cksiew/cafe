using CMS.CommonLib.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Cafes.Queries.GetCafes
{
    public record GetCafesQuery(PaginationRequest PaginationRequest):IQuery<GetCafesResult>;

    public record GetCafesResult(PaginatedResult<CafeDto> Cafes);
    
}
