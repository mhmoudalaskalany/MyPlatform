using Entities.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Identity
{
    public class AppConfig : IEntityTypeConfiguration<App>
    {
        public void Configure(EntityTypeBuilder<App> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(e => e.UserRoles)
                .WithOne(p => p.App)
                .HasForeignKey(e => e.AppId);

            builder.Property(a => a.Code).HasMaxLength(255).IsRequired();

            builder.HasIndex(a => a.Code).IsUnique();
        }
    }
}
