
namespace CMS.API.Endpoints
{
    public record UpdateCafeRequest(CafeCreateUpdateDto Cafe);

    public record UpdateCafeResponse(bool IsSuccess);

    public class UpdateCafe : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            //app.MapPut("/cafes", async (UpdateCafeRequest request, ISender sender) =>
            //{
            //    var updateCommand = request.Adapt<UpdateCafeCommand>();

            //    var result = await sender.Send(updateCommand);

            //    var response = result.Adapt<UpdateCafeResponse>();

            //    return Results.Ok(response);

            //})
            //     .WithName("UpdateCafe")
            //     .Produces<UpdateCafeResponse>(StatusCodes.Status200OK)
            //     .ProducesProblem(StatusCodes.Status400BadRequest)
            //     .WithSummary("Update Cafe")
            //     .WithDescription("Update Cafe");

            app.MapPut("/cafes", async (HttpRequest req, ISender sender) =>
            {
                var form = await req.ReadFormAsync(); // Read form data
                var logoFile = form.Files.GetFile("logoFile"); // Get the LogoFile from the form
                var name = form["name"];
                var description = form["description"];
                var location = form["location"];
                var id = form["id"];

                if (string.IsNullOrEmpty(name))
                {
                    return Results.BadRequest("Id is required.");
                }

                if (string.IsNullOrEmpty(name))
                {
                    return Results.BadRequest("Name is required.");
                }

                if (string.IsNullOrEmpty(description))
                {
                    return Results.BadRequest("Description is required.");
                }

                if (string.IsNullOrEmpty(location))
                {
                    return Results.BadRequest("Location is required.");
                }


                // Create the CafeCreateDto manually from the form data
                var cafeUpdateDto = new CafeCreateUpdateDto(
                    Guid.Parse(id!), // Assuming you're generating a new Guid for the cafe
                    name!,
                    description!,
                    logoFile!,        // Assign the IFormFile LogoFile
                    location!
                );


                var updateCommand = new UpdateCafeCommand(cafeUpdateDto);  // Adapt to command

                var result = await sender.Send(updateCommand);

                var response = result.Adapt<UpdateCafeResponse>();

                return Results.Ok(response);

            })
                 .WithName("UpdateCafe")
                 .Produces<UpdateCafeResponse>(StatusCodes.Status200OK)
                 .ProducesProblem(StatusCodes.Status400BadRequest)
                 .WithSummary("Update Cafe")
                 .WithDescription("Update Cafe");
        }
    }
}
