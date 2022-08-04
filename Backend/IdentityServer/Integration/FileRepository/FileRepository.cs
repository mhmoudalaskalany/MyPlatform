using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Helper.HttpClient;
using Domain.Helper.HttpClient.RestSharp;
using Integration.FileRepository.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using TokenDto = Integration.FileRepository.Dtos.TokenDto;

namespace Integration.FileRepository
{
    public class FileRepository : IFileRepository
    {
        #region Properties
        private readonly IRestSharpContainer _restSharpContainer;
        private readonly MicroServicesUrls _urls;
        private readonly IConfiguration _configuration;
        #endregion


        #region Constructors

        public FileRepository(IRestSharpContainer restSharpContainer, IConfiguration configuration, MicroServicesUrls urls)
        {
            _restSharpContainer = restSharpContainer;
            _configuration = configuration;
            _urls = urls;
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Get Tokens For Files
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public async Task<List<TokenDto>> GetTokens(List<Guid> ids , string appCode)
        {
            var result = await _restSharpContainer.SendRequest<ResponseResult>(_urls.GenerateTokenWithClaims + "/" + appCode, Method.POST, ids);
            var tokens = JsonConvert.DeserializeObject<List<TokenDto>>(JsonConvert.SerializeObject(result.Data));
            return tokens;
        }
        /// <summary>
        /// Delete File
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteFile(Guid id)
        {
            await _restSharpContainer.SendRequest<ResponseResult>(_urls.Delete + "/" + id, Method.GET);
        }

        /// <summary>
        /// Upload File As  Array of Bytes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<UploadResponseDto> UploadBytes(UploadRequestDto dto)
        {
            var result = await _restSharpContainer.SendRequest<ResponseResult>(_urls.UploadBytes, Method.POST, dto);
            return JsonConvert.DeserializeObject<UploadResponseDto>(JsonConvert.SerializeObject(result.Data));
        }


        #endregion




    }
}
