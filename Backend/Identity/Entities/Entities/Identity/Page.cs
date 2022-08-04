﻿using System.Collections.Generic;
using Entities.Entities.Base;

namespace Entities.Entities.Identity
{
    public class Page : BaseEntity
    {
        public long Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public long AppId { get; set; }
        public App App { get; set; }
        public long? ParentId { get; set; }
        public Page Parent { get; set; }
        public string Code { get; set; }
        public virtual ICollection<PagePermission> PagePermissions { get; set; } = new List<PagePermission>();
        
    }
}
