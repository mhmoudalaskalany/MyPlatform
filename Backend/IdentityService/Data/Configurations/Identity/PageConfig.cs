using Entities.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Identity
{
    public class PageConfig : IEntityTypeConfiguration<Page>
    {
        public void Configure(EntityTypeBuilder<Page> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(p => p.Parent)
                .WithMany()
                .HasForeignKey(e => e.ParentId);
            builder.Property(a => a.Code).HasMaxLength(255).IsRequired();

            builder.HasIndex(a => a.Code).IsUnique();
        }
    }
}
