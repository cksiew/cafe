using CMS.CommonLib.Utilities;
using CMS.Domain.Abstractions;
using CMS.Domain.Enums;
using CMS.Domain.ValueObjects;

namespace CMS.Domain.Models
{
    public class Employee : Entity<EmployeeId>
    {
        private const int UID_LENGTH = 7;

        public string Name { get; private set; } = default!;
        public EmailAddress EmailAddress { get; private set; } = default!;
        public PhoneNumber PhoneNumber { get; private set; } = default!;

        public Gender Gender { get; private set; } = Gender.Unknown;

        public DateTime? StartDate { get; private set; } = default!;

        public CafeId? CafeId { get; set; } 

        public Cafe Cafe { get; set; } = default!;

        public int DaysOfWork
        {
            get
            {
                if (CafeId != null && StartDate != null)
                {
                    var difference = DateTime.UtcNow - StartDate.Value;
                    return difference.Days;
                }
                return 0;
            }
        }

        public static Employee Create(string name, 
            EmailAddress emailAddress, 
            PhoneNumber phoneNumber, 
            Gender gender,
            CafeId? cafeId
            )
        {
            return new Employee
            {
                Id = EmployeeId.Of("UI" +  IdentifierGenerator.GenerateUIDFromCurrentDate(UID_LENGTH)),
                Name = name,
                EmailAddress = emailAddress,
                PhoneNumber = phoneNumber,
                Gender = gender,
                CafeId = cafeId,
                StartDate = cafeId != null? DateTime.UtcNow : null
            };
        }

        public void Update(string name, EmailAddress emailAddress, PhoneNumber phoneNumber, Gender gender)
        {
            Name = name;
            EmailAddress = emailAddress;
            PhoneNumber = phoneNumber;
            Gender = gender;
        }

        public void StartWork(CafeId? cafeId, DateTime? startDate)
        {
            if (cafeId == null)
            {
                StartDate = null;
            }
            else if (CafeId != cafeId)
            {
                StartDate = startDate.HasValue? startDate: DateTime.UtcNow;
            }
            
        }
    }
}
