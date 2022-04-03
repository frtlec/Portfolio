using MediatR;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Domain.WorkAggregate;
using Portfolio.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Commands
{
    public class SaveGeneralSettingCommand:IRequest<Response<List<SaveGeneralSettingsDto>>>
    {
        public List<GeneralSetting> Settings { get; set; }
    }
}
