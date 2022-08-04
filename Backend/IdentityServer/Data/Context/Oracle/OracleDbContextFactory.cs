using Data.Context.Factory;
using Microsoft.EntityFrameworkCore;

namespace Data.Context.Oracle
{
    public class OracleDbContextFactory : DesignTimeDbContextFactoryBase<OracleDbContext>
    {
        protected override OracleDbContext CreateNewInstance(DbContextOptions<OracleDbContext> options)
        {
            return new OracleDbContext(options);
        }
    }
}
