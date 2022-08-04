using System.Collections.Generic;
using Entities.Entities;
using Entities.Entities.Identity;

namespace Data.DataInitializer
{
    public class DataInitializer : IDataInitializer
    {

        public Permission[] SeedPermissions()
        {
            var permissionList = new List<Permission>();
            permissionList.AddRange(new[]
            {
                new Permission
                {
                    Id = 1,
                    NameEn = "Add",
                    NameAr = "اضافة",
                    Code = "Add"
                },
                new Permission
                {
                    Id = 2,
                    NameEn = "Edit",
                    NameAr = "تعديل",
                    Code = "Edit"
                },
                new Permission
                {
                    Id = 3,
                    NameEn = "View",
                    NameAr = "عرض",
                    Code = "View"
                },
                new Permission
                {
                    Id = 4,
                    NameEn = "Delete",
                    NameAr = "حذف",
                    Code = "Delete"
                }

            });

            return permissionList.ToArray();
        }
    }
}
