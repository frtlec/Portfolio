using AutoMapper;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Portfolio.Api.Mail.Dtos;
using Portfolio.Api.Mail.Infrastructure;
using Portfolio.Api.Mail.Models;
using Portfolio.Api.Mail.Validations.FluentValidation;
using Portfolio.Shared.Dtos;
using Portfolio.Shared.Extensions;
using Portfolio.Shared.RabbitMQ.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mass = MassTransit;

namespace Portfolio.Api.Mail.Services
{
  public class ContactService : IContactService
  {
    private readonly MailDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly Mass.IPublishEndpoint _publishEndpoint;
    private readonly IMailSettingService _mailSettingService;
    public ContactService(MailDbContext dbContext, IMapper mapper, Mass.IPublishEndpoint publishEndpoint, IMailSettingService mailSettingService)
    {
      _dbContext = dbContext;
      _mapper = mapper;
      _publishEndpoint = publishEndpoint;
      _mailSettingService = mailSettingService;
    }
    public async Task<Response<List<ContactDto>>> GetAll()
    {
      var contacts = await _dbContext.Contacts.ToListAsync();

      if (contacts.Any() == false)
      {
        contacts = new List<Contact>();
      }

      return Response<List<ContactDto>>.Success(_mapper.Map<List<ContactDto>>(contacts), 200);
    }
    public async Task<Response<NoContent>> AddContact(AddContactDto addContactDto)
    {
      ValidationResult validationResult = new AddContactDtoValidator().Validate(addContactDto);
      if (validationResult.IsValid == false)
        return Response<NoContent>.Fail(validationResult.Errors.FluentValidationErrorToListString(), 400);

      var mailSettings =await _mailSettingService.GetByEmail(addContactDto.FromMail);
      if (mailSettings.IsSuccessful==false)
      {
        return Response<NoContent>.Fail(mailSettings.Errors, 400);
      }

      var newContact = _mapper.Map<Contact>(addContactDto);
      newContact.Id = Guid.NewGuid().ToString();
      newContact.CreatedDate = DateTime.Now;
      _dbContext.Contacts.Add(newContact);
      await _dbContext.SaveChangesAsync();

      await _publishEndpoint.Publish<ContactMailSendCommand>(
         new ContactMailSendCommand
         {
           ContactId = newContact.Id
         });
      return Response<NoContent>.Success(200);
    }

    public async Task<Response<ContactDto>> GetById(string contactId)
    {
      Contact contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);
      ContactDto mappedContact = _mapper.Map<ContactDto>(contact);
      return Response<ContactDto>.Success(mappedContact, 200);
    }

    public async Task<Response<NoContent>> SuccessSentMailAfterContactUpdate(string contactId)
    {
      Contact contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);
      contact.IsSent = true;
      contact.SuccessFullSentDate = DateTime.Now;
      await _dbContext.SaveChangesAsync();

      return Response<NoContent>.Success(200);
    }
  }
}
