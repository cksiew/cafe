using CMS.Application.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CMS.Application.Cafes.Commands.CreateCafe
{
    public class CreateCafeHandler(IApplicationDbContext dbContext, ApplicationConfiguration appConfig) : ICommandHandler<CreateCafeCommand, CreateCafeResult>
    {
        public async Task<CreateCafeResult> Handle(CreateCafeCommand command, CancellationToken cancellationToken)
        {
            var targetDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var imagesDirectory = Path.Combine(targetDirectory!, appConfig.UploadedImageDirectory);
            var imageFilePath = Path.Combine(imagesDirectory, Path.GetFileName(command.Cafe.LogoFile.FileName));

            Directory.CreateDirectory(Path.GetDirectoryName(imagesDirectory)!);

            using (var stream = new FileStream(imageFilePath, FileMode.Create))
            {
                await command.Cafe.LogoFile.CopyToAsync(stream, cancellationToken);
            }

                var cafe = CreateNewCafe(command.Cafe);
            dbContext.Cafes.Add(cafe);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new CreateCafeResult(cafe.Id.Value);
        }

        private Cafe CreateNewCafe(CafeCreateUpdateDto cafeDto)
        {
            var newCafe = Cafe.Create(
                id: CafeId.Of(Guid.NewGuid()),
                name: cafeDto.Name,
                description: cafeDto.Description,
                location: cafeDto.Location,
                logo: appConfig!.UploadedImageHostPath + "/" +cafeDto.LogoFile.FileName
                );


            return newCafe;
        }
    }

}
