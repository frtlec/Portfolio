using Porfolio.Services.Setting.API.Models.DbModels;
using System.Collections.Generic;

namespace Porfolio.Services.Setting.API.Models.Dtos
{
  public class AboutPageDto
  {
    public string PortreFileName { get; set; }
    public string Slogan { get; set; }
    public string Summary { get; set; }
    public List<AboutSoftwareDto> Softwares { get; set; }
    public List<AboutBusinessDto> Businesses { get; set; }
    public List<AboutEducationDto> Educations { get; set; }
    public List<AboutCertifacateDto> Certifacates { get; set; }
    public List<AboutProjectDto> Projects { get; set; }
    public bool? Active { get; set; }
    public string Id { get; set; }
  }
  public class AboutSoftwareDto: CommonAboutAggregate
  {
    public string SvgPath { get; set; }
    public string SoftwareName { get; set; }
  }
  public class AboutBusinessDto : CommonAboutAggregate
  {
    public string Head { get; set; }
    public string Value { get; set; }
    public string Foot { get; set; }
  }
  public class AboutEducationDto : CommonAboutAggregate
  {
    public string Head { get; set; }
    public string Value { get; set; }
    public string Foot { get; set; }
 
  }
  public class AboutCertifacateDto : CommonAboutAggregate
  {
    public string Head { get; set; }
    public string Value { get; set; }
 
  }
  public class AboutProjectDto : CommonAboutAggregate
  {
    public string Head { get; set; }
    public string Value { get; set; }
    public string Link { get; set; }
 
  }
}
