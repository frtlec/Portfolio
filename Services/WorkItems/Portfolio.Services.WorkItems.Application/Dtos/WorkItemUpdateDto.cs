using Portfolio.Shared.Enums;
using System.Collections.Generic;

namespace Portfolio.Services.WorkItems.Application.Dtos
{
    public class WorkItemUpdateDto
    {
        public int Id { get; set; }
        public List<string> Pictures { get; set; }
        public WorkItemsTemplateType TemplateType { get; set; }
        public string Title { get; set; }
        public int WorkId { get; set; }
        public string Description { get; set; }
    }
}
