using AutoMapper;
using FluentValidation.Results;
using MongoDB.Driver;
using Portfolio.Services.MailSender.Dtos;
using Portfolio.Services.MailSender.Models;
using Portfolio.Services.MailSender.Settings;
using Portfolio.Services.MailSender.Validations.FluentValidation;
using Portfolio.Shared.Dtos;
using Portfolio.Shared.Extensions;
using Portfolio.Shared.RabbitMQ.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Mass = MassTransit;

namespace Portfolio.Services.MailSender.Services
{
    public class ContactService: IContactService
    {
        private readonly IMongoCollection<Contact> _contactCollection;
        private readonly IMapper _mapper;
        private readonly Mass.IPublishEndpoint _publishEndpoint;
        public ContactService(IDataBaseSettings dataBaseSettings, IMapper mapper, Mass.IPublishEndpoint publishEndpoint)
        {
            var client = new MongoClient(dataBaseSettings.ConnectionString);
            var dataBase = client.GetDatabase(dataBaseSettings.DatabaseName);
            _contactCollection = dataBase.GetCollection<Contact>(dataBaseSettings.ContactCollectionName);


            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }
        public async Task<Response<List<ContactDto>>> GetAll()
        {
            var contacts = await _contactCollection.Find(_=>true).ToListAsync();

            if (contacts.Any()==false)
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
            var newContact = _mapper.Map<Contact>(addContactDto);
            newContact.CreatedDate = DateTime.Now;
            await _contactCollection.InsertOneAsync(newContact);

            await _publishEndpoint.Publish<ContactMailSendCommand>(
               new ContactMailSendCommand
               {
                    ContactId = newContact.Id
               });
            return Response<NoContent>.Success(200);
        }

        public async Task<Response<ContactDto>> GetById(string contactId)
        {
            Contact contact=await _contactCollection.Find(x=>x.Id==contactId).FirstOrDefaultAsync();
            ContactDto mappedContact =_mapper.Map<ContactDto>(contact);
            return Response<ContactDto>.Success(mappedContact,200);
        }

        public async Task<Response<NoContent>> SuccessSentMailAfterContactUpdate(string contactId)
        {
            Contact contact= await _contactCollection.Find(x => x.Id == contactId).FirstOrDefaultAsync();
            contact.IsSent=true;
            contact.SuccessFullSentDate=DateTime.Now;
            await _contactCollection.FindOneAndReplaceAsync(x=>x.Id==contactId, contact);

            return Response<NoContent>.Success(200);
        }
    }
}
