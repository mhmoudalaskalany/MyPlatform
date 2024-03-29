﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities.Entities.Base;

namespace Entities.Entities.Hr
{
    public class Nationality : BaseEntity
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
    }
}
