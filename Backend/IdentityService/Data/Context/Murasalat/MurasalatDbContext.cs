using Data.Configurations.Views;
using Microsoft.EntityFrameworkCore;
using Entities.Entities.Murasalat.Hr;
using Entities.Entities.Views.Murasalat;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Data.Context.Murasalat
{
    public partial class MurasalatDbContext : DbContext
    {
        public MurasalatDbContext()
        {
        }

        public MurasalatDbContext(DbContextOptions<MurasalatDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MawredLeaves> MawredLeaves { get; set; }
        public virtual DbSet<MurasalatEmployeeView> MurasalatEmployees { get; set; }
        public virtual DbSet<OrganizationHierarchyView> MurasalatUnits { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MawredLeaves>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Creationdate)
                    .HasColumnName("creationdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Endate)
                    .HasColumnName("endate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Filenumber)
                    .IsRequired()
                    .HasColumnName("filenumber")
                    .HasMaxLength(50);

                entity.Property(e => e.LeaveId).HasColumnName("leaveId");

                entity.Property(e => e.LeaveTypeId).HasColumnName("Leave_Type_ID");

                entity.Property(e => e.Leavetype)
                    .HasColumnName("leavetype")
                    .HasMaxLength(50);

                entity.Property(e => e.Startdate)
                    .HasColumnName("startdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TransferDone).HasColumnName("transferDone");

                entity.Property(e => e.TransferStatus)
                    .HasColumnName("transferStatus")
                    .HasMaxLength(50);

                entity.Property(e => e.Transferdate)
                    .HasColumnName("transferdate")
                    .HasColumnType("datetime");
            });

            modelBuilder.ApplyConfiguration(new EmployeeViewConfig());
            modelBuilder.ApplyConfiguration(new OrganizationHierarchyViewConfig());
            base.OnModelCreating(modelBuilder);
        }

    }
}
