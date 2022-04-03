using AutoMapper;
using MongoDB.Driver;
using Portfolio.Services.MailSender.Dtos;
using Portfolio.Services.MailSender.Models;
using Portfolio.Services.MailSender.Settings;
using Portfolio.Shared.Dtos;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender.Services
{
    public class MailSettingService: IMailSettingService
    {
        private readonly IMongoCollection<MailSetting> _mailSettingCollection;
        private readonly IMapper _mapper;
        public MailSettingService(IDataBaseSettings dataBaseSettings, IMapper mapper)
        {
            var client = new MongoClient(dataBaseSettings.ConnectionString);
            var dataBase = client.GetDatabase(dataBaseSettings.DatabaseName);
            _mailSettingCollection = dataBase.GetCollection<MailSetting>(dataBaseSettings.MailSettingCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<MailSettingDto>> GetById(string mailSettingId)
        {
            MailSetting mailSetting=  await _mailSettingCollection.Find(f => f.Id == mailSettingId).FirstOrDefaultAsync();

            return Response<MailSettingDto>.Success(_mapper.Map<MailSettingDto>(mailSetting),200);
        }
        public async Task<Response<NoContent>> Create(CreateMailSettingDto createMailSettingDto)
        {
            bool isExistsMailSetting = await _mailSettingCollection.Find(f => f.Mail == createMailSettingDto.Mail).AnyAsync();

            if (isExistsMailSetting)
            {
                return Response<NoContent>.Fail("This record already exists",400);
            }

            await _mailSettingCollection.InsertOneAsync(_mapper.Map<MailSetting>(createMailSettingDto));

            return Response<NoContent>.Success(200);
        }

        public async Task<Response<MailSettingDto>> GetByEmail(string email)
        {
            MailSetting mailSetting = await _mailSettingCollection.Find(f => f.Mail == email).FirstOrDefaultAsync();

            return Response<MailSettingDto>.Success(_mapper.Map<MailSettingDto>(mailSetting), 200);
        }
    }
}
