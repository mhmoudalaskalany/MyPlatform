﻿using System;
using System.Collections.Generic;

namespace Domain.DTO.Hr.Unit
{
    public class UnitChildrenDto
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public List<Guid> ChildrenIds { get; set; } = new List<Guid>();
    }
}
