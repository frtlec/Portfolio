using Portfolio.Services.WorkItems.Domain.Core;
using Portfolio.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Domain.WorkAggregate
{
    public class WorkItem : Entity
    {
        public List<string> Pictures { get; set; }
        public WorkItemsTemplateType TemplateType { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatorUserId { get; set; }
        public int WorkId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedDate { get; private set; }
        public DateTime? UpdatedDate { get; private set; }

        public WorkItem(List<string> pictures, WorkItemsTemplateType templateType, string title, string desc, int creatorUserId, int workId)
        {
            Pictures = pictures;
            TemplateType = templateType;
            Title = title;
            Description = desc;
            CreatorUserId = creatorUserId;
            CreatedDate = DateTime.Now;
            WorkId = workId;

        }
        public WorkItem()
        {

        }
        public void UpdateWorkItem(List<string> pictures, WorkItemsTemplateType templateType, string title, string desc)
        {
            Pictures = pictures;
            TemplateType = templateType;
            Title = title;
            Description = desc;
            UpdatedDate = DateTime.Now;
        }
    }
}
