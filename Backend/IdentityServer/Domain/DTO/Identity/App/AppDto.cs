﻿using System;
using Domain.Core;

namespace Domain.DTO.Identity.App
{
    public class AppDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string AppUrl { get; set; }
        public string AppIcon { get; set; }
        public string AppColor { get; set; }
        public long UsersCount { get; set; }
        public bool IsPublic { get; set; }
        public string Code { get; set; }
    }
}
