using Entities.Entities.Hr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Hr
{
    public class EmployeeTeamConfig : IEntityTypeConfiguration<EmployeeTeam>
    {
        public void Configure(EntityTypeBuilder<EmployeeTeam> builder)
        {
            builder.HasKey(ua => new { ua.EmployeeId, ua.TeamId });
        }
    }
}
