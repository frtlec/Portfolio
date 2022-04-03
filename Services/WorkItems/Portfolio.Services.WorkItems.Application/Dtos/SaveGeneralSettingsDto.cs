using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Dtos
{
    public class SaveGeneralSettingsDto
    {
        public Domain.WorkAggregate.SettingType Type { get; set; }
        public bool Success { get; set; }
    }
}
