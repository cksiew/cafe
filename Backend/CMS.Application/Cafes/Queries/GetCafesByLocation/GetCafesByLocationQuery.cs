using CMS.CommonLib.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Cafes.Queries.GetCafesByLocation
{
    public record GetCafesByLocationQuery(string Location, PaginationRequest PaginationRequest):IQuery<GetCafesByLocationResult>;

    public record GetCafesByLocationResult(PaginatedResult<CafeDto> Cafes);
}
