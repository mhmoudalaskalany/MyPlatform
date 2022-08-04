using Entities.Entities.Views.Oracle;
using Microsoft.EntityFrameworkCore;

namespace Data.Context.Oracle
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options)
        {


        }

        #region View Models

        public DbSet<EmployeeLeaveView> MawredLeaves { get; set; }

        #endregion


        #region Protected Ovrriden Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<EmployeeLeaveView>(
                    eb =>
                    {
                        eb.HasNoKey();
                        eb.ToView("MAWRED_DM_EMP_LEAVES");
                    });
            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region Public Overriden Methods



        #endregion

        #region Private Methods



        #endregion





    }
}
