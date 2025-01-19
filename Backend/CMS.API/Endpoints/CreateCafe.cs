
namespace CMS.API.Endpoints
{

    public record CreateCafeRequest(CafeCreateUpdateDto Cafe);

    public record CreateCafeResponse(Guid Id);

    public class CreateCafe : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/cafes", async (HttpRequest req, ISender sender) =>
            {
                var form = await req.ReadFormAsync(); 
                var logoFile = form.Files.GetFile("logoFile"); 
                var name = form["name"];
                var description = form["description"];
                var location = form["location"];

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

                if (logoFile == null)
                {
                    return Results.BadRequest("Logo file is required.");
                }

                // Create the CafeCreateDto manually from the form data
                var cafeCreateDto = new CafeCreateUpdateDto(
                    Guid.Empty,  // Assuming you're generating a new Guid for the cafe
                    name!,
                    description!,
                    logoFile,        // Assign the IFormFile LogoFile
                    location!
                );



                var command = new CreateCafeCommand(cafeCreateDto);  // Adapt to command

                var result = await sender.Send(command);  // Send the command

                var response = result.Adapt<CreateCafeResponse>();  // Adapt to response

                return Results.Created($"/cafes/{response.Id}", response);
            })
            .WithName("CreateCafe")
            .Produces<CreateCafeResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Cafe")
            .WithDescription("Create Cafe");
        }
    }
}
