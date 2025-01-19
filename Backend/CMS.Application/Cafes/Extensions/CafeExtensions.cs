using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Cafes.Extensions
{
    public static class CafeExtensions
    {
        public static IEnumerable<CafeDto> ToCafeDtoList(this IEnumerable<Cafe> cafes)
        {
            return cafes.Select(c =>
            {
                return DtoFromCafe(c);
            });
        }

        public static CafeDto ToCafeDto(this Cafe cafe)
        {
            return DtoFromCafe(cafe);
        }

        private static CafeDto DtoFromCafe(Cafe cafe)
        {
            return new CafeDto(
                Id: cafe.Id.Value,
                Name: cafe.Name,
                Description: cafe.Description,
                Logo: cafe.Logo??"",
                Location: cafe.Location,
                EmployeesCount: cafe.Employees.Count()
                );
        }
    }
}
