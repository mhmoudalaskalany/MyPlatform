﻿using System;

namespace Entities.Entities.Views.Murasalat
{
    public class MurasalatEmployeeView
    {
        public Guid Id { get; set; }
        public string FileNumber { get; set; }
        public string CivilNumber { get; set; }
        public string ArCurrentFirstName { get; set; }
        public string ArCurrentSecondName { get; set; }
        public string ArCurrentMiddleName { get; set; }
        public string ArCurrentLastName { get; set; }
        public string ArFullName { get; set; }
        public string EnCurrentFirstName { get; set; }
        public string EnCurrentSecondNamee { get; set; }
        public string EnCurrentMiddleName { get; set; }
        public string EnCurrentLastName { get; set; }
        public string EnFullName { get; set; }
        public string Phone { get; set; }
        public string ArPositiontype { get; set; }
        public string EnPositiontype { get; set; }
        public string DepartmentCode { get; set; }
        public string ArDepartmentName { get; set; }
        public string EnDepartmentName { get; set; }
        public string DirectManagerFileNumber { get; set; }
        public string DirectManagerCivilNumber { get; set; }
        public string ArDirectManagerName { get; set; }
        public string EnDirectManagerName { get; set; }
        public string ParentDepartmentCode { get; set; }
        public string ArParentDepartmentName { get; set; }
        public string EnParentDepartmentName { get; set; }
        public string GrandParentDepartmentCode { get; set; }
        public string ArGrandParentDepartmentName { get; set; }
        public string EnGrandParentDepartmentName { get; set; }
        public string GrandDepartmentCode { get; set; }
        public string ArGrandDepartmentName { get; set; }
        public string EnGrandDepartmentName { get; set; }
        public DateTime? HiringDate { get; set; }
        public int IsManager { get; set; }
        public Guid? ManagerId { get; set; }

    }
}