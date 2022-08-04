using Entities.Entities.Views.Murasalat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Views
{
    public class EmployeeViewConfig : IEntityTypeConfiguration<MurasalatEmployeeView>
    {
        public void Configure(EntityTypeBuilder<MurasalatEmployeeView> builder)
        {
            builder.HasNoKey();
            builder.ToView("vw_ProgSec");
        }
    }
}
