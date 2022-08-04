using Entities.Entities.Hr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Hr
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Id)
                .ValueGeneratedNever();
            builder.HasIndex(e => e.NationalId).IsUnique();
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasOne(x => x.Manager)
                .WithMany(c => c.ManagerEmployees)
                .HasForeignKey(x => x.ManagerId);


        }
    }
}
