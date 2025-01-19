using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Cafes.Commands.DeleteCafe
{
    public class DeleteCafeHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteCafeCommand, DeleteCafeResult>
    {
        public async Task<DeleteCafeResult> Handle(DeleteCafeCommand command, CancellationToken cancellationToken)
        {
            var cafeId = CafeId.Of(command.CafeId);
            
            var cafe = await dbContext.Cafes.FindAsync([cafeId], cancellationToken) ?? throw new NotFoundException(nameof(Cafe), command.CafeId);
            dbContext.Cafes.Remove(cafe);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteCafeResult(true);
        }
    }
}
