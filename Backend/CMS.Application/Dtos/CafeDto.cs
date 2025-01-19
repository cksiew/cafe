using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Dtos
{
    public record CafeDto(Guid Id, string Name, string Description, string Logo, string Location, int EmployeesCount);

    public record CafeCreateUpdateDto(Guid Id, string Name, string Description, IFormFile LogoFile, string Location);
    
}
