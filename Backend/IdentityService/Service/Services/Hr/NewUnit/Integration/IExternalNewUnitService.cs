﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.FullUnit;
using Service.Services.Base;

namespace Service.Services.Hr.NewUnit.Integration
{
    public interface IExternalNewUnitService : IBaseService<Entities.Entities.Hr.FullUnit, AddFullUnitDto, FullUnitDto , string>
    {
        /// <summary>
        /// Get Unit Parent ( Used In Stock )
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetUnitParentAsync(string childId);
        /// <summary>
        /// Get All Units
        /// </summary>
        /// <returns></returns>
        Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);
        /// <summary>
        /// Get Units By Ids (Used In Stock)
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetUnitsByIdsAsync(List<string> unitIds);
        /// <summary>
        /// Get All Departments
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetDropDownForDepartmentAsync(BaseParam<SearchCriteriaFilter> filter);
    }
}

