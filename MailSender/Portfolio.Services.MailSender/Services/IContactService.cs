using Portfolio.Services.MailSender.Dtos;
using Portfolio.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Services.MailSender.Services
{
    public interface IContactService
    {
        Task<Response<List<ContactDto>>> GetAll();
        Task<Response<ContactDto>> GetById(string contactId);
        Task<Response<NoContent>> AddContact(AddContactDto addContactDto);
        Task<Response<NoContent>> SuccessSentMailAfterContactUpdate(string contactId);
    }
}
