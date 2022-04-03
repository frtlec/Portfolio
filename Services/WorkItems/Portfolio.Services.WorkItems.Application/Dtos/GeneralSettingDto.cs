using Portfolio.Services.WorkItems.Domain.WorkAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Dtos
{
    public class GeneralSettingDto
    {
        public string Value { get; set; }
        public SettingType SettingType { get; set; }
        public bool IsActive { get; set; }
    }
}
