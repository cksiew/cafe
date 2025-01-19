
using CMS.Domain.Enums;

namespace CMS.Application.Dtos
{
    public record EmployeeDto(string? Id, string Name, Gender Gender, string EmailAddress, string PhoneNumber, int? DaysWork, string CafeId, string? CafeName);
}
