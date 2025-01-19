using CMS.Application.Cafes.Commands.DeleteCafe;

namespace CMS.API.Endpoints
{

    public record DeleteCafeResponse(bool IsSuccess);
    public class DeleteCafe : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/cafes/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteCafeCommand(id));

                var response = result.Adapt<DeleteCafeResponse>();

                return Results.Ok(response);

            })
               .WithName("DeleteCafe")
               .Produces<DeleteCafeResponse>(StatusCodes.Status200OK)
               .ProducesProblem(StatusCodes.Status400BadRequest)
               .WithSummary("Delete Cafe")
               .WithDescription("Delete Cafe");
        }
    }
}
