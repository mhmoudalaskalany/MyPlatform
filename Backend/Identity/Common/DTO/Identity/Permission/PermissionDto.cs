﻿using Common.Core;

namespace Common.DTO.Identity.Permission
{
    public class PermissionDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool? IsSelected { get; set; }
    }
}
