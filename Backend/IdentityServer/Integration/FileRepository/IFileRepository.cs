using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Integration.FileRepository.Dtos;
using TokenDto = Integration.FileRepository.Dtos.TokenDto;

namespace Integration.FileRepository
{
    public interface IFileRepository
    {
        /// <summary>
        /// Get Tokens
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="appCode"></param>
        /// <returns></returns>
        Task<List<TokenDto>> GetTokens(List<Guid> ids , string appCode);
        /// <summary>
        /// Delete File
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteFile(Guid id);

        /// <summary>
        /// Upload File As  Array of Bytes
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<UploadResponseDto> UploadBytes(UploadRequestDto dto);
    }
}
