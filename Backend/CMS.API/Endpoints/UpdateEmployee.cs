
using CMS.Application.Employees.Commands.UpdateEmployee;

namespace CMS.API.Endpoints
{
    public record UpdateEmployeeRequest(EmployeeDto Employee);

    public record UpdateEmployeeResponse(bool IsSuccess);

    public class UpdateEmployee : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/employees", async (UpdateEmployeeRequest request, ISender sender) =>
            {
                var updateCommand = request.Adapt<UpdateEmployeeCommand>();

                var result = await sender.Send(updateCommand);

                var response = result.Adapt<UpdateEmployeeResponse>();

                return Results.Ok(response);

            })
                 .WithName("UpdateEmployee")
                 .Produces<UpdateEmployeeResponse>(StatusCodes.Status200OK)
                 .ProducesProblem(StatusCodes.Status400BadRequest)
                 .WithSummary("Update Employee")
                 .WithDescription("Update Employee");
        }
    }
}
