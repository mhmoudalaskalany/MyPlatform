using Entities.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Identity
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            builder.Property(a => a.Code).HasMaxLength(255).IsRequired();

            builder.HasIndex(a => a.Code).IsUnique();
        }
    }
}
