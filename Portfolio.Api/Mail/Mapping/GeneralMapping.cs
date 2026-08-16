using AutoMapper;
using Portfolio.Api.Mail.Dtos;
using Portfolio.Api.Mail.Models;

namespace Portfolio.Api.Mail.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Contact, AddContactDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();
            CreateMap<MailSetting, MailSettingDto>().ReverseMap();
            CreateMap<MailSetting, CreateMailSettingDto>().ReverseMap();
        }
    }
}
