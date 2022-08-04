using Entities.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Identity
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedNever();
            builder.Property(a => a.Code).HasMaxLength(255).IsRequired();

            builder.HasIndex(a => a.Code).IsUnique();
        }
    }
}
