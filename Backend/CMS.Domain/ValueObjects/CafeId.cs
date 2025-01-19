using CMS.Domain.Exceptions;

namespace CMS.Domain.ValueObjects
{
    public record CafeId
    {
        public Guid Value { get; }

        private CafeId(Guid value) => Value = value;

        public static CafeId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (value == Guid.Empty)
            {
                throw new DomainException("CafeId cannot be empty.");
            }

            return new CafeId(value);
        }
    }
}
