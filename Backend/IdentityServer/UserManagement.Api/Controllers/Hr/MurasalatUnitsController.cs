using System;
using System.Threading.Tasks;
using Domain.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.MurasalatUnit;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// Murasalat Units Controller
    /// </summary>
    public class MurasalatUnitsController : BaseController
    {
        private readonly IMurasalatUnitServiceData _unitService;
        /// <summary>
        /// Constructor
        /// </summary>
        public MurasalatUnitsController(IMurasalatUnitServiceData unitService)
        {
            _unitService = unitService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetFromView/{id:guid}")]
        public async Task<IFinalResult> GetFromViewAsync(Guid id)
        {
            var result = await _unitService.GetByIdFromViewAsync(id);
            return result;
        }

        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("Get/{id:guid}")]
        public async Task<IFinalResult> GetAsync(Guid id)
        {
            var result = await _unitService.GetByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IFinalResult> GetAllAsync()
        {
            var result = await _unitService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Get All No Duplication 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllDistinct")]
        public async Task<IFinalResult> GetNonDuplicateAllAsync()
        {
            var result = await _unitService.GetNonDuplicateAllAsync();
            return result;
        }

        /// <summary>
        /// Get All No Duplication 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("UpdateAllDistinct")]
        public async Task<IFinalResult> UpdateNonDuplicateAllAsync()
        {
            var result = await _unitService.UpdateNonDuplicateAllAsync();
            return result;
        }
    }
}
