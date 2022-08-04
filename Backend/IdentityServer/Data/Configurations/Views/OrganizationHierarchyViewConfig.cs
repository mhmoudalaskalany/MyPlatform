using Entities.Entities.Views.Murasalat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations.Views
{
    public class OrganizationHierarchyViewConfig : IEntityTypeConfiguration<OrganizationHierarchyView>
    {
        public void Configure(EntityTypeBuilder<OrganizationHierarchyView> builder)
        {
            builder.HasNoKey();
            builder.ToView("ProgSec_vw_OrganizationHierarchy");
        }
    }
}
