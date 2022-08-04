using System;
using Entities.Entities.Hr;
using Entities.Entities.Views.Murasalat;

//ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapMurasalatEmployee()
        {
           

            CreateMap<MurasalatEmployeeView, FullEmployee>()
                .ForMember(dest => dest.IsManager, opt =>
                    opt.MapFrom(src => Convert.ToBoolean(src.IsManager)))
                .ForMember(dest => dest.AttachmentId, opt =>
                    opt.Ignore())
                .ForMember(dest => dest.PhotoId, opt =>
                    opt.Ignore())
                .ForMember(dest => dest.IpPhone, opt =>
                    opt.Ignore())
                .ForMember(dest => dest.IsVaccinated, opt =>
                    opt.Ignore())
                .ForMember(dest => dest.DoseStatus, opt =>
                    opt.Ignore())
                .ForMember(dest => dest.Email, opt =>
                    opt.Ignore())
                .ReverseMap();


           
        }
    }
}