
using CMS.Application.Employees.Commands.DeleteEmployee;

namespace CMS.API.Endpoints
{

    public record DeleteEmployeeResponse(bool IsSuccess);


    public class DeleteEmployee : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/employees/{id}", async (string id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteEmployeeCommand(id));

                var response = result.Adapt<DeleteEmployeeResponse>();

                return Results.Ok(response);

            })
               .WithName("DeleteEmployee")
               .Produces<DeleteEmployeeResponse>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Delete Employee")
               .WithDescription("Delete Employee");
        }
    }
}
