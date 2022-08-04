using Data.Context.Factory;
using Microsoft.EntityFrameworkCore;

namespace Data.Context.Murasalat
{
    public class MurasalatDbContextFactory : DesignTimeDbContextFactoryBase<MurasalatDbContext>
    {
        protected override MurasalatDbContext CreateNewInstance(DbContextOptions<MurasalatDbContext> options)
        {
            return new MurasalatDbContext(options);
        }
    }
}
