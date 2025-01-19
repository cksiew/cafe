namespace CMS.Infrastructure.Data.Configurations
{
    public class CafeConfiguration : IEntityTypeConfiguration<Cafe>
    {
        public void Configure(EntityTypeBuilder<Cafe> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id").HasConversion(
                cafeId => cafeId.Value,
                dbId => CafeId.Of(dbId));
            builder.Property(p => p.Name).HasColumnName("name").IsRequired();
            builder.Property(p => p.Description).HasColumnName("description").IsRequired();
            builder.Property(p => p.Logo).HasColumnName("logo");
            builder.Property(p => p.Location).HasColumnName("location").IsRequired();
            
        }
    }
}
