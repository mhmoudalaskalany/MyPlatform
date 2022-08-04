using Data.Context.Factory;
using Microsoft.EntityFrameworkCore;

namespace Data.Context.Identity
{
    public class IdentityDbContextFactory : DesignTimeDbContextFactoryBase<IdentityServerDbContext>
    {
        protected override IdentityServerDbContext CreateNewInstance(DbContextOptions<IdentityServerDbContext> options)
        {
            return new IdentityServerDbContext(options);
        }
    }
}
