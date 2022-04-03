using Portfolio.Services.WorkItems.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Domain.WorkAggregate
{
    public class About : Entity, IAggregateRoot
    {
        public string Value { get; set; }
        public AboutType AboutType { get; set; }
        public bool IsActive { get; set; }
        public string BeforeValue { get; set; } 
    }
    public enum AboutType 
    {
        PICTURE,
        SUMMARY,
        EDUCATION,
        WORK
    }
}
