using CMS.Application.Configurations;
using System.Reflection;

namespace CMS.Application.Cafes.Commands.UpdateCafe
{
    public class UpdateCafeHandler(IApplicationDbContext dbContext, ApplicationConfiguration appConfig) : ICommandHandler<UpdateCafeCommand, UpdateCafeResult>
    {
        public async Task<UpdateCafeResult> Handle(UpdateCafeCommand command, CancellationToken cancellationToken)
        {
            var cafeDto = command.Cafe;
            if (cafeDto.LogoFile != null)
            {
                var targetDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var imagesDirectory = Path.Combine(targetDirectory!, appConfig.UploadedImageDirectory);
                var imageFilePath = Path.Combine(imagesDirectory, Path.GetFileName(command.Cafe.LogoFile.FileName));

                Directory.CreateDirectory(Path.GetDirectoryName(imagesDirectory)!);

                using (var stream = new FileStream(imageFilePath, FileMode.Create))
                {
                    await command.Cafe.LogoFile.CopyToAsync(stream, cancellationToken);
                }
            }
           
            var cafeId = CafeId.Of(cafeDto.Id);
            var cafe = await dbContext.Cafes
                .FirstOrDefaultAsync(c => c.Id == cafeId, cancellationToken) ?? throw new NotFoundException(nameof(Cafe), command.Cafe.Id);
            var logo = string.Empty;
            if (cafeDto.LogoFile == null)
            {
                logo = cafe.Logo;
            } else
            {
                logo = appConfig.UploadedImageHostPath + "/" + cafeDto.LogoFile?.FileName;
            }

            cafe.Update(
                name: cafeDto.Name,
                description: cafeDto.Description,
                location: cafeDto.Location,
                logo: logo!
                );

            dbContext.Cafes.Update(cafe);
            int result = await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateCafeResult(result == 1);
        }
    }
}
