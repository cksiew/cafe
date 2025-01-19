
using CMS.Application.Employees.Commands.CreateEmployee;

namespace CMS.API.Endpoints
{

    public record CreateEmployeeRequest(EmployeeDto Employee);

    public record CreateEmployeeResponse(string Id);

    public class CreateEmployee : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/employees", async (CreateEmployeeRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateEmployeeCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateEmployeeResponse>();

                return Results.Created($"/employees/{response.Id}", response);
            })
                .WithName("CreateEmployee")
                .Produces<CreateEmployeeResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Employee")
                .WithDescription("Create Employee");
        }
    }
}
