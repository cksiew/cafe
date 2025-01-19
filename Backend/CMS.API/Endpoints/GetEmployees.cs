
using CMS.Application.Employees.Queries.GetEmployees;
using CMS.Application.Employees.Queries.GetEmployeesByCafe;
using CMS.CommonLib.Pagination;
using Microsoft.Identity.Client;

namespace CMS.API.Endpoints
{

    public record GetEmployeesResponse(PaginatedResult<EmployeeDto> Employees);

    public class GetEmployees : BaseEndpoint, ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/employees", async (string? cafeId, [AsParameters] PaginationRequest request, ISender sender) =>
            {
                object response;

                if (!string.IsNullOrEmpty(cafeId))
                {
                    response = await HandleRequest<GetEmployeesResponse>(new GetEmployeesByCafeQuery(cafeId,request), sender);
                } else
                {
                    response = await HandleRequest<GetEmployeesResponse>(new GetEmployeesQuery(request), sender);
                }

                return Results.Ok(response);
            })
                .WithName("GetEmployeesByCafe")
                .Produces<GetEmployeesResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Get Employees By Cafe")
                .WithDescription("Get Employees By Cafe");
        }
    }
}
