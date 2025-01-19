using CMS.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace CMS.Domain.ValueObjects
{
    public record PhoneNumber
    {
        public string Value { get; } = default!;

        private static bool IsValid(string phoneNumber)
        {
            var phoneNumberRegex = new Regex(@"^[89][0-9]{7}$");
            return phoneNumberRegex.IsMatch(phoneNumber);
        }

        private PhoneNumber(string phoneNumber)
        {
            Value = phoneNumber;
        }

        public static PhoneNumber Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            if (!IsValid(value))
                throw new DomainException("Phone Number is invalid.");

            return new PhoneNumber(value);
        }

        public override string ToString() => $"{Value}";
    }
}
