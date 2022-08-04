using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Hr
{
    public class FullEmployeeConfig : IEntityTypeConfiguration<Entities.Entities.Hr.FullEmployee>
    {
        public void Configure(EntityTypeBuilder<Entities.Entities.Hr.FullEmployee> modelBuilder)
        {
            modelBuilder.HasOne(e => e.Attachment)
                .WithOne(e => e.FullEmployee)
                .HasForeignKey<Entities.Entities.Hr.FullEmployee>(a => a.AttachmentId);

            modelBuilder.HasOne(e => e.Unit)
                .WithMany(e => e.Employees)
                .HasForeignKey(a => a.DepartmentCode);
            
        }
    }
}
