using Entities.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Identity
{
    public class UserAppConfig : IEntityTypeConfiguration<UserApp>
    {
        public void Configure(EntityTypeBuilder<UserApp> builder)
        {
            builder.HasKey(ua => new { ua.UserId, ua.AppId });
        }
    }
}
