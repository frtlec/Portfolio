using AutoMapper;
using Portfolio.Services.MailSender.Dtos;
using Portfolio.Services.MailSender.Models;

namespace Portfolio.Services.MailSender.Mapping
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
