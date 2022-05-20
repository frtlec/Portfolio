using AutoMapper;
using MongoDB.Driver;
using Porfolio.Services.Setting.API.Models.DbModels;
using Porfolio.Services.Setting.API.Models.Dtos;
using Porfolio.Services.Setting.API.Services.Interfaces;
using Porfolio.Services.Setting.API.Settings;
using Portfolio.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Porfolio.Services.Setting.API.Services
{
  public class AboutPageSettingService : IAboutPageSettingService
  {
    private readonly IMongoCollection<AboutPage> _aboutPageCollection;
    private readonly IMapper _mapper;
    public AboutPageSettingService(IDataBaseSettings dataBaseSettings, IMapper mapper)
    {
      var client = new MongoClient(dataBaseSettings.ConnectionString);
      var dataBase = client.GetDatabase(dataBaseSettings.DatabaseName);
      _aboutPageCollection = dataBase.GetCollection<AboutPage>(dataBaseSettings.AboutPageCollectionName);
      _mapper = mapper;
    }
    public async Task<Response<AboutPage>> GetAllByActive(bool? isActive)
    {
      try
      {
        AboutPage aboutPage = await _aboutPageCollection.Find(_ => true).FirstOrDefaultAsync();
        if (aboutPage==null)
        {
          return Response<AboutPage>.Success(new AboutPage(), 200);
        }

        if (isActive != null)
        {
          aboutPage.Softwares = aboutPage.Softwares.ToList();
          aboutPage.Projects = aboutPage.Projects.ToList();
          aboutPage.Educations = aboutPage.Educations.ToList();
          aboutPage.Certifacates = aboutPage.Certifacates.ToList();
        }

        return Response<AboutPage>.Success(aboutPage, 200);
      }
      catch (System.Exception ex)
      {
        return Response<AboutPage>.Fail(ex.Message, 500);
      }
    }

    public async Task<Response<AboutPage>> MultiAddOrUpdate(AboutPageDto aboutPageDto)
    {
      try
      {
        AboutPage aboutPage = await _aboutPageCollection.Find(_=>true).FirstOrDefaultAsync();
    
        if (aboutPage == null)
        {
          aboutPage = _mapper.Map<AboutPage>(aboutPageDto);

          aboutPage.CreatedDate = DateTime.Now;
          aboutPage.CreatedUserId = 1;
          await _aboutPageCollection.InsertOneAsync(aboutPage);
        }
        else
        {
          aboutPage.Slogan = aboutPage.Slogan;
          aboutPage.Summary = aboutPageDto.Summary;
          aboutPage.Softwares = _mapper.Map<List<AboutSoftware>>(aboutPageDto.Softwares);
          aboutPage.Businesses = _mapper.Map<List<AboutBusiness>>(aboutPageDto.Businesses);
          aboutPage.Educations = _mapper.Map<List<AboutEducation>>(aboutPageDto.Educations);
          aboutPage.Certifacates = _mapper.Map<List<AboutCertifacate>>(aboutPageDto.Certifacates);
          aboutPage.Projects = _mapper.Map<List<AboutProject>>(aboutPageDto.Projects);
          aboutPage.UpdatedDate = DateTime.Now;
          aboutPage.UpdatedUserId = 1;
          aboutPage.Active=aboutPageDto.Active;
          aboutPage.PortreFileName= aboutPageDto.PortreFileName;
          await _aboutPageCollection.FindOneAndReplaceAsync(f=>f.Id== aboutPageDto.Id, aboutPage);
        }
       

        return Response<AboutPage>.Success(aboutPage, 200);
      }
      catch (System.Exception ex)
      {
        return Response<AboutPage>.Fail(ex.Message, 500);
      };
    }
  }
}
