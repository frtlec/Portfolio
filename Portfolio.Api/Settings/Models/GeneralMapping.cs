using AutoMapper;
using Portfolio.Api.Settings.Models.DbModels;
using Portfolio.Api.Settings.Models.Dtos;

namespace Portfolio.Api.Settings.Models
{
  public class GeneralMapping : Profile
  {
    public GeneralMapping()
    {
      


      CreateMap<AboutPage, AboutPageDto>().ReverseMap();
      CreateMap<AboutProject, AboutProjectDto>().ReverseMap();
      CreateMap<AboutEducation, AboutEducationDto>().ReverseMap();
      CreateMap<AboutSoftware, AboutSoftwareDto>().ReverseMap();
      CreateMap<AboutCertifacate, AboutCertifacateDto>().ReverseMap();
      CreateMap<AboutBusiness, AboutBusinessDto>().ReverseMap();
      CreateMap<Localization, LocalizationAddDto>().ReverseMap();
      CreateMap<Localization, LocalizationUpdateDto>().ReverseMap();



    }
  }
}
