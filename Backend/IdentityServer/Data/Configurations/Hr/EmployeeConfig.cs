using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Hr
{
    public class EmployeeConfig : IEntityTypeConfiguration<Entities.Entities.Hr.Employee>
    {
        public void Configure(EntityTypeBuilder<Entities.Entities.Hr.Employee> modelBuilder)
        {
            
        }
    }
}
