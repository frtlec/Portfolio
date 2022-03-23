using Portfolio.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Dtos
{
    public class WorkItemDto
    {
        public List<string> Pictures { get; set; }
        public WorkItemsTemplateType TemplateType { get; set; }
        public string Title { get; set; }
        public int WorkId { get; set; }
        public string Description { get; set; }
    }
}
