using CMS.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace CMS.Domain.ValueObjects
{
    public record EmployeeId
    {
        public string Value { get; }

        private EmployeeId(string value) => Value = value;

        public static EmployeeId Of(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == string.Empty)
            {
                throw new DomainException("EmployeeId cannot be empty.");
            }
            if (!CheckEmployeeIdFormat(value)) {
                throw new DomainException("EmployeeId is not in the correct format.");
            }

            return new EmployeeId(value);
        }

        private static bool CheckEmployeeIdFormat(string value)
        {
            var match = Regex.Match(value, @"UI[a-zA-Z0-9]{7}$");
            return match.Success;
        }
    }
}
