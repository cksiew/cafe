namespace CMS.Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasConversion(
                employeeId => employeeId.Value,
                dbId => EmployeeId.Of(dbId));

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .IsRequired();

            builder.Property(p => p.EmailAddress)
                .HasColumnName("email_address")
                .HasConversion(
                emailAddress => emailAddress.Value,
                dbEmailAddress => EmailAddress.Of(dbEmailAddress))
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasColumnName("phone_number")
                .HasConversion(
                phoneNumber => phoneNumber.Value,
                dbPhoneNumber => PhoneNumber.Of(dbPhoneNumber))
                .IsRequired();

            builder.Property(p => p.Gender)
                .HasColumnName("gender")
                .HasDefaultValue(Gender.Male)
                .HasConversion(
                    g => g.ToString(),
                    db => (Gender)Enum.Parse(typeof(Gender), db))
                .IsRequired();

            builder.Property(p => p.StartDate)
                .HasColumnName("start_date");

            builder.HasOne(e=>e.Cafe)
                .WithMany(c=>c.Employees)
                .HasForeignKey(e => e.CafeId)
                .OnDelete(DeleteBehavior.Cascade);


        }


    }
}
