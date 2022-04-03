using Portfolio.Services.WorkItems.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Domain.WorkAggregate
{
    public class GeneralSetting:Entity,IAggregateRoot
    {
        public string Value { get; set; }
        public SettingType SettingType { get; set; }
        public bool IsActive { get; set; }
    }
    public enum SettingType
    {
        EMPTY,
        SOCIAL,
        HOMEPAGEITEMLIMIT,
        HOMEPAGECONTAINERCSS,
        RIGHTMENUBACKGROUND
    }
}
