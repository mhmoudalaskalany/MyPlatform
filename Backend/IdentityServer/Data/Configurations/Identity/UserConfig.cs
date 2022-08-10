using Entities.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Identity
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasIndex(e => e.UserName).IsUnique();
            builder.Property(e => e.AccessFailedCount).HasDefaultValue(0);
            builder.Property(e => e.EmailConfirmed).HasDefaultValue(0);
            builder.Property(e => e.PhoneNumberConfirmed).HasDefaultValue(0);
            builder.Property(e => e.TwoFactorEnabled).HasDefaultValue(0);

        }
    }
}
