using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Mail.Dtos;
using Portfolio.Api.Mail.Infrastructure;
using Portfolio.Api.Mail.Models;
using Portfolio.Shared.Dtos;
using System;
using System.Threading.Tasks;

namespace Portfolio.Api.Mail.Services
{
  public class MailSettingService : IMailSettingService
  {
    private readonly MailDbContext _dbContext;
    private readonly IMapper _mapper;
    public MailSettingService(MailDbContext dbContext, IMapper mapper)
    {
      _dbContext = dbContext;
      _mapper = mapper;
    }

    public async Task<Response<MailSettingDto>> GetById(string mailSettingId)
    {

      MailSetting mailSetting = await _dbContext.MailSettings.FirstOrDefaultAsync(f => f.Id == mailSettingId);

      return Response<MailSettingDto>.Success(_mapper.Map<MailSettingDto>(mailSetting), 200);
    }
    public async Task<Response<NoContent>> Create(CreateMailSettingDto createMailSettingDto)
    {
      bool isExistsMailSetting = await _dbContext.MailSettings.AnyAsync(f => f.Mail == createMailSettingDto.Mail);

      if (isExistsMailSetting)
      {
        return Response<NoContent>.Fail("This record already exists", 400);
      }

      MailSetting newMailSetting = _mapper.Map<MailSetting>(createMailSettingDto);
      newMailSetting.Id = Guid.NewGuid().ToString();
      _dbContext.MailSettings.Add(newMailSetting);
      await _dbContext.SaveChangesAsync();

      return Response<NoContent>.Success(200);
    }

    public async Task<Response<MailSettingDto>> GetByEmail(string email)
    {
      MailSetting mailSetting = await _dbContext.MailSettings.FirstOrDefaultAsync(f => f.Mail == email);
      if (mailSetting==null)
      {
        return Response<MailSettingDto>.Fail($"{email} Mail Settings notfound!", 404);
      }
      return Response<MailSettingDto>.Success(_mapper.Map<MailSettingDto>(mailSetting), 200);
    }
  }
}
