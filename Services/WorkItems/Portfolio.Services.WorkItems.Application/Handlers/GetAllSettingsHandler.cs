using MediatR;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Application.Mapping;
using Portfolio.Services.WorkItems.Application.Queries;
using Portfolio.Services.WorkItems.Domain.WorkAggregate;
using Portfolio.Services.WorkItems.Infrastructure;
using Portfolio.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Handlers
{
    public class GetAllSettingsHandler:IRequestHandler<GetAllSettingsQuery,Response<List<GeneralSettingDto>>>
    {
        private readonly WorkItemsDbContext _context;

        public GetAllSettingsHandler(WorkItemsDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<GeneralSettingDto>>> Handle(GetAllSettingsQuery request, CancellationToken cancellationToken)
        {
            {
                try
                {
                    List<GeneralSetting> generalSettings = _context.GeneralSettings.ToList();
                    List<GeneralSettingDto> result = ObjectMapper.Mapper.Map<List<GeneralSettingDto>>(generalSettings);
                    return Response<List<GeneralSettingDto>>.Success(result, 200);
                }
                catch (Exception ex)
                {
                    return Response<List<GeneralSettingDto>>.Fail("error", 500);
                }
            }
        }
    }
}
