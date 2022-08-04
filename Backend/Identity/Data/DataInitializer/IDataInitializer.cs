using Entities.Entities;
using Entities.Entities.Identity;

namespace Data.DataInitializer
{
    public interface IDataInitializer
    {

        Permission[] SeedPermissions();
    }
}
