using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Core;
using Common.Helper.HttpClient;
using Common.Helper.HttpClient.RestSharp;
using Integration.FileRepository.Dtos;
using Newtonsoft.Json;
using RestSharp;
using TokenDto = Integration.FileRepository.Dtos.TokenDto;

namespace Integration.FileRepository
{
    public class FileRepository : IFileRepository
    {

        private readonly IRestSharpContainer _restSharpContainer;
        private readonly MicroServicesUrls _urls;



        public FileRepository(IRestSharpContainer restSharpContainer,  MicroServicesUrls urls)
        {
            _restSharpContainer = restSharpContainer;
            _urls = urls;
        }




        public async Task<List<TokenDto>> GetTokens(List<Guid> ids, string appCode)
        {
            var result = await _restSharpContainer.SendRequest<ResponseResult>(_urls.GenerateTokenWithClaims + "/" + appCode, Method.Post, ids);
            var tokens = JsonConvert.DeserializeObject<List<TokenDto>>(JsonConvert.SerializeObject(result.Data));
            return tokens;
        }

        public async Task DeleteFile(Guid id)
        {
            await _restSharpContainer.SendRequest<ResponseResult>(_urls.Delete + "/" + id, Method.Get);
        }

        public async Task<UploadResponseDto> UploadBytes(UploadRequestDto dto)
        {
            var result = await _restSharpContainer.SendRequest<ResponseResult>(_urls.UploadBytes, Method.Post, dto);
            return JsonConvert.DeserializeObject<UploadResponseDto>(JsonConvert.SerializeObject(result.Data));
        }







    }
}
