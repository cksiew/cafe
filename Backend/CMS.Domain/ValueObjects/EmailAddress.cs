using CMS.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace CMS.Domain.ValueObjects
{
    public record EmailAddress
    {
        public string Value { get; } = default!;

        
        private EmailAddress(string email)
        {
            Value = email;
        }

        public static EmailAddress Of(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);
            if (!IsValidEmail(value))
            {
                throw new DomainException("Email is invalid");
            }

            return new EmailAddress(value);
        }

        public override string ToString() => $"{Value}";

        private static bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }


    }
}
