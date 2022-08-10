using System;
using System.Collections.Generic;
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
                    Id = Guid.NewGuid(),
                    NameEn = "Add",
                    NameAr = "اضافة",
                    Code = "Add"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    NameEn = "Edit",
                    NameAr = "تعديل",
                    Code = "Edit"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    NameEn = "View",
                    NameAr = "عرض",
                    Code = "View"
                },
                new Permission
                {
                    Id = Guid.NewGuid(),
                    NameEn = "Delete",
                    NameAr = "حذف",
                    Code = "Delete"
                }

            });

            return permissionList.ToArray();
        }
    }
}
