using AutoMapper;
using FluentValidation.Results;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Porfolio.Services.Setting.API.Models.DbModels;
using Porfolio.Services.Setting.API.Models.Dtos;
using Porfolio.Services.Setting.API.ModelValidator;
using Porfolio.Services.Setting.API.Services.Interfaces;
using Porfolio.Services.Setting.API.Settings;
using Portfolio.Shared.Dtos;
using Portfolio.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Porfolio.Services.Setting.API.Services
{
  public class LocalizationService : ILocalizationService
  {
    private readonly IMongoCollection<Localization> _localizationCollection;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private const string CACHE_KEY = "SelinOzogluLocalization";
    public LocalizationService(IDataBaseSettings dataBaseSettings, IMapper mapper, IMemoryCache memoryCache)
    {
      var client = new MongoClient(dataBaseSettings.ConnectionString);
      var dataBase = client.GetDatabase(dataBaseSettings.DatabaseName);
      _localizationCollection = dataBase.GetCollection<Localization>(dataBaseSettings.LocalizationCollectionName);
      _mapper = mapper;
      _memoryCache = memoryCache;
    }
    public async Task<Response<Localization>> Add(LocalizationAddDto localization)
    {
      try
      {
        ValidationResult validationResult = new LocalizationAddDtoValidator().Validate(localization);
        if (validationResult.IsValid == false)
          return Response<Localization>.Fail(validationResult.Errors.FluentValidationErrorToListString(), 400);

        var exists=await _localizationCollection.FindAsync(f=>f.Key==localization.Key);
        if (exists.Any())
        {
          return Response<Localization>.Fail("this record already exists", 400);
        }


        Localization insertModel = _mapper.Map<Localization>(localization);
        insertModel.CreatedDate = DateTime.Now;
        insertModel.CreatedUserId = 1;
        await _localizationCollection.InsertOneAsync(insertModel);
        _memoryCache.Remove(CACHE_KEY);
        return Response<Localization>.Success(insertModel, 200);
      }
      catch (System.Exception ex)
      {
        return Response<Localization>.Fail(ex.Message, 500);
      }
    }

    public async Task<Response<NoContent>> Delete(string id)
    {
      try
      {
        await _localizationCollection.DeleteOneAsync(f=>f.Id== id);
        _memoryCache.Remove(CACHE_KEY);
        return Response<NoContent>.Success(200);
      }
      catch (System.Exception ex)
      {
        return Response<NoContent>.Fail(ex.Message, 500);
      }
    }

    public async Task<Response<List<Localization>>> GetAll()
    {
      try
      {
        List<Localization> localizations = await _localizationCollection.Find(_=>true).ToListAsync();

        _memoryCache.Remove(CACHE_KEY);
        _memoryCache.Set(CACHE_KEY, localizations);

        return Response<List<Localization>>.Success(localizations, 200);
      }
      catch (System.Exception ex)
      {
        return Response<List<Localization>>.Fail(ex.Message, 500);
      }
    }

    public async Task<Response<LocalizationGetByCultureDtoResponse>> GetByCulture(LocalizationGetByCultureDto getByCultureDto)
    {
      LocalizationGetByCultureDtoResponse localizationGetByCultureDtoResponse = new();
      try
      {
        if (getByCultureDto.LocalizationType==0 || string.IsNullOrEmpty(getByCultureDto.Key) )
        {
          localizationGetByCultureDtoResponse.Value = getByCultureDto.Key;
          return Response<LocalizationGetByCultureDtoResponse>.Success(localizationGetByCultureDtoResponse, 200);
        }
        //x => x.Key.ToLower().StripHTML() == getByCultureDto.Key.ToLower().StripHTML() && x.LocalizationType == getByCultureDto.LocalizationType;


        List<Localization> cachedlocalizations=_memoryCache.Get<List<Localization>>(CACHE_KEY);

        List<Localization> localizations = cachedlocalizations;
        if (localizations == null || localizations.Count()<0)
        {
          localizations= await _localizationCollection.Find(x => x.Key != null).ToListAsync();
        }
          
        IQueryable<Localization> query = localizations.AsQueryable();
        Localization localization = query.Where(x =>
        x.Key.ToLower().RemoveHtmlTags().RemoveLines().RemoveSpaces() == getByCultureDto.Key.ToLower().RemoveHtmlTags().RemoveLines().RemoveSpaces() && 
        x.LocalizationType == getByCultureDto.LocalizationType
        ).FirstOrDefault();
        if (localization==null)
        {
          localizationGetByCultureDtoResponse.Value=getByCultureDto.Key;
          return Response<LocalizationGetByCultureDtoResponse>.Success(localizationGetByCultureDtoResponse, 200);
        }
        localizationGetByCultureDtoResponse.Value= localization.Value;
        return Response<LocalizationGetByCultureDtoResponse>.Success(localizationGetByCultureDtoResponse, 200);
      }
      catch (System.Exception ex)
      {
        localizationGetByCultureDtoResponse.Message = ex.Message;
        localizationGetByCultureDtoResponse.Value = getByCultureDto.Key;
        return Response<LocalizationGetByCultureDtoResponse>.Success(localizationGetByCultureDtoResponse, 200);
      }
    }

    public Task<Response<Localization>> Update(LocalizationUpdateDto localization)
    {
      throw new System.NotImplementedException();
    }
   

  }
}
