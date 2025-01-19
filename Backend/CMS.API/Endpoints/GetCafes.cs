using CMS.Application.Cafes.Queries.GetCafes;
using CMS.Application.Cafes.Queries.GetCafesByLocation;
using CMS.CommonLib.Pagination;
using CMS.Domain.ValueObjects;

namespace CMS.API.Endpoints
{
    public record GetCafesResponse(PaginatedResult<CafeDto> Cafes);

    public class GetCafes : BaseEndpoint, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapGet("/cafes", async (string? location, [AsParameters] PaginationRequest request, ISender sender) =>
            {
                object response;

                if (!string.IsNullOrEmpty(location))
                {

                    response = await HandleRequest<GetCafesResponse>(new GetCafesByLocationQuery(location, request), sender);
                } else
                {
                    response = await HandleRequest<GetCafesResponse>(new GetCafesQuery(request), sender);
                }

                return Results.Ok(response);
            })
                .WithName("GetCafesByLocation")
                .Produces<GetCafesResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Get Cafes By Location")
                .WithDescription("Get Cafes By Location");

        }

       
    }
}
