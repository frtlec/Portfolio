using AutoMapper;
using Porfolio.Services.Setting.API.Models.DbModels;
using Porfolio.Services.Setting.API.Models.Dtos;

namespace Porfolio.Services.Setting.API.Models
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



    }
  }
}
