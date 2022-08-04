using Entities.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Identity
{
    public class PagePermissionConfig:IEntityTypeConfiguration<PagePermission>
    {
        public void Configure(EntityTypeBuilder<PagePermission> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
