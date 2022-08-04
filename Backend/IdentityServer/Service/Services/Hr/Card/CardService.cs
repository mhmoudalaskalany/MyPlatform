using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Common.Template;
using Domain.DTO.Hr.Card;
using Domain.DTO.Hr.FullEmployee;
using Domain.Helper.HttpClient;
using Entities.Enum;
using Integration.FileRepository;
using Integration.FileRepository.Dtos;
using Service.Services.Base;
using Service.Services.Common.Template;

namespace Service.Services.Hr.Card
{
    public class CardService : BaseService<Entities.Entities.Hr.Card, AddCardDto, CardDto, long?>, ICardService
    {
        private readonly MicroServicesUrls _urls;
        private readonly ITemplateService _templateService;
        private readonly IFileRepository _fileRepository;
        public CardService(IServiceBaseParameter<Entities.Entities.Hr.Card> parameters, ITemplateService templateService, IFileRepository fileRepository, MicroServicesUrls urls) : base(parameters)
        {
            _templateService = templateService;
            _fileRepository = fileRepository;
            _urls = urls;
        }

        #region Public Methods
        /// <summary>
        /// Get Cards By Employee Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<IResult> GetByEmployeeIdAsync(Guid employeeId)
        {
            var entities = await UnitOfWork.Repository.FindAsync(x => x.EmployeeId == employeeId);
            var data = Mapper.Map<List<CardDto>>(entities);
            await GetCardsTokens(data);
            return ResponseResult.PostResult(data, HttpStatusCode.OK);
        }
        /// <summary>
        /// Add New Card
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<byte[]> AddCardAsync(AddCardDto model)
        {

            var template = await GenerateTemplate(model.Employee, model.CardType , model.HiringDate);
            var uploadBytesDto = new UploadRequestDto
            {
                FileBytes = template,
                AppCode = Configuration["AppCodes:HR"],
                CategoryCode = Configuration["Files:Categories:WORk-CARD"],
                AccessLevelCode = Configuration["Files:Levels:PRIVATE"],
                AttachmentExtension = "PDF",
                FileName = $"Card-{model.Employee.FullNameAr}-{model.Employee.NationalId}",
                IsPublic = false,
                MimeType = "application/pdf",
                StorageType = StorageType.LocalStorage
            };
            var fileResult = await _fileRepository.UploadBytes(uploadBytesDto);
            var card = new Entities.Entities.Hr.Card
            {
                NationalId = model.Employee.NationalId,
                EmployeeId = model.Employee.Id.Value,
                IsActive = true,
                CreatedById = long.Parse(ClaimData.UserId),
                CreatedDate = DateTime.Now,
                FileId = fileResult.FileId,
                CardNumber = model.CardNumber
            };
            SetEntityCreatedBaseProperties(card);
            UnitOfWork.Repository.Add(card);
            var affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                return template;
            }

            return null;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Generate Template
        /// </summary>
        /// <returns></returns>
        private async Task<byte[]> GenerateTemplate(MurasalatEmployeeDto employee, CardType type, DateTime? hiringDate = null)
        {
            var templateCode = type == CardType.Plain ? Configuration["Templates:Card_Template_No_Word"] : Configuration["Templates:Card_Template"];
            //var photoUrl = _urls.DownloadWithId + "/" + employee.PhotoId + "?appCode=" + Configuration["AppCodes:HR"];
           
            var name = employee.FullNameAr;
            var modifiedName = "";
            if (name.Contains("الفاضل/ "))
            {


                modifiedName = name.Replace("الفاضل/ ", "");

            }
            else if (name.Contains("الفاضل /"))
            {


                modifiedName = name.Replace("الفاضل /", "");

            }
            else if (name.Contains("الفاضلة /"))
            {


                modifiedName = name.Replace("الفاضلة /", "");

            }
            else if (name.Contains("الفاضلة/ "))
            {


                modifiedName = name.Replace("الفاضلة/ ", "");

            }
            else
            {
                modifiedName = name;
            }
            var date = employee.HiringDate?.ToString(format: "d/M/yyyy");
            if (employee.HiringDate == null)
            {
                date = hiringDate?.ToString(format: "d/M/yyyy");
            }



            var parameters = new TemplateDto
            {
                TemplateCode = templateCode,
                Parameters = new Dictionary<string, dynamic>
                {
                    {"Job", employee.Position},
                    {"Name", modifiedName},
                    {"NameEn", employee.FullNameEn },
                    {"PhotoUrl" , employee.PhotoUrl},
                    {"Date", date },
                    {"NationalId", employee.NationalId },
                    {"FileNumber", employee.FileNumber }
                }
            };
            var template = await _templateService.GetCardTemplateAsBytesByCode(parameters);
            return template;

        }

        private async Task GetCardsTokens(List<CardDto> data)
        {
            var ids = data.Select(x => x.FileId).ToList();
            if (ids.Count > 0)
            {
                var tokenDtos = await _fileRepository.GetTokens(ids, Configuration["AppCodes:HR"]);
                data.ForEach(e =>
                {
                    var tokenDto = tokenDtos.First(x => x.Id == e.FileId);
                    e.CardUrl = _urls.DownloadFileWithAppCode + "/" + tokenDto.Id + "?token=" + tokenDto.Token;
                });
            }

        }

        #endregion






    }

}
