using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Settings.Infrastructure;
using Portfolio.Api.Settings.Models.DbModels;
using Portfolio.Api.Settings.Models.Dtos;
using Portfolio.Api.Settings.Services.Interfaces;
using Portfolio.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Api.Settings.Services
{
  public class AboutPageSettingService : IAboutPageSettingService
  {
    private readonly SettingsDbContext _dbContext;
    private readonly IMapper _mapper;
    public AboutPageSettingService(SettingsDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }
    public async Task<Response<AboutPage>> GetAllByActive(bool? isActive)
    {
      try
      {
        AboutPage aboutPage = await _dbContext.AboutPages.FirstOrDefaultAsync();
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
        AboutPage aboutPage = await _dbContext.AboutPages.FirstOrDefaultAsync();

        if (aboutPage == null)
        {
          aboutPage = _mapper.Map<AboutPage>(aboutPageDto);

          aboutPage.Id = Guid.NewGuid().ToString();
          aboutPage.CreatedDate = DateTime.Now;
          aboutPage.CreatedUserId = 1;
          _dbContext.AboutPages.Add(aboutPage);
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
        }

        await _dbContext.SaveChangesAsync();

        return Response<AboutPage>.Success(aboutPage, 200);
      }
      catch (System.Exception ex)
      {
        return Response<AboutPage>.Fail(ex.Message, 500);
      };
    }
  }
}
