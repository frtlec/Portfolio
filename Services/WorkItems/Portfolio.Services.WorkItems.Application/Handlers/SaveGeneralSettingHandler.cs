using FluentValidation.Results;
using MediatR;
using Portfolio.Services.WorkItems.Application.Commands;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Application.Validations.FluentValidation;
using Portfolio.Services.WorkItems.Domain.WorkAggregate;
using Portfolio.Services.WorkItems.Infrastructure;
using Portfolio.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Handlers
{
    public class SaveGeneralSettingHandler : IRequestHandler<SaveGeneralSettingCommand, Response<List<SaveGeneralSettingsDto>>>
    {
        private readonly WorkItemsDbContext _context;

        public SaveGeneralSettingHandler(WorkItemsDbContext context)
        {
            _context = context;
        }
        public async Task<Response<List<SaveGeneralSettingsDto>>> Handle(SaveGeneralSettingCommand request, CancellationToken cancellationToken)
        {

            try
            {

                List<SaveGeneralSettingsDto> result = new List<SaveGeneralSettingsDto>();

                foreach (var item in request.Settings)
                {
                    SaveGeneralSettingsDto saveGeneralSettingsDto=new SaveGeneralSettingsDto();
                    GeneralSetting generalSettings = _context.GeneralSettings.FirstOrDefault(f => f.Id == item.Id);

                    if (generalSettings != null)
                    {
                        generalSettings.IsActive = item.IsActive;
                        generalSettings.Value = item.Value;
                        generalSettings.SettingType = item.SettingType;
                        _context.GeneralSettings.Update(generalSettings);
                    }
                    else
                    {
                        generalSettings = new GeneralSetting();
                        generalSettings.IsActive = item.IsActive;
                        generalSettings.Value = item.Value;
                        generalSettings.SettingType = item.SettingType;
                        await _context.GeneralSettings.AddAsync(generalSettings);
                    }
                    await _context.SaveChangesAsync();

                    saveGeneralSettingsDto.Type = item.SettingType;
                    saveGeneralSettingsDto.Success = true;
                    result.Add(saveGeneralSettingsDto);


                }

                return Response<List<SaveGeneralSettingsDto>>.Success(result, 200);
            }
            catch (Exception ex)
            {
                return Response<List<SaveGeneralSettingsDto>>.Fail("Error", 500);
            }
        }
    }
}
