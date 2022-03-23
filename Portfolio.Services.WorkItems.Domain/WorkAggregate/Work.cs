using Portfolio.Services.WorkItems.Domain.Core;
using Portfolio.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Domain.WorkAggregate
{
    public class Work : Entity, IAggregateRoot
    {
        [MaxLength(200)]
        [Required]
        public string MainPicture { get; set; } 
        [MaxLength(150)]
        [Required]
        public string Title { get; set; }
        [MaxLength(200)]
        [Required]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public short CategoryId { get; set; }
        [Required]
        public int CreatorUserId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getdate()")]
        public DateTime? CreatedDate { get; private set; }
        public DateTime? UpdatedDate { get; private set; }

        private readonly List<WorkItem> _workItems;
        public IReadOnlyCollection<WorkItem> WorkItems => _workItems;
        public Category Category { get; set; }

        public Work()
        {

        }
        public Work(string mainPicture, string title, string description, int creatorUserId,bool isActive,short categoryId)
        {
            _workItems = new List<WorkItem>();

            CreatedDate = DateTime.Now;
            MainPicture = mainPicture;
            Title = title;
            Description = description;
            CreatorUserId = creatorUserId;
            IsActive = isActive;
            CategoryId = categoryId;
        }
        public (bool,string) AddWorkItem(List<string> pictures, WorkItemsTemplateType templateType, string title, string desc, int creatorUserId,int workId)
        {
            var existWorkItem = _workItems.Any(x => x.Title == Title);
            if (existWorkItem)
            {
                return (false, "data duplication is blocked");
            }

            var newOrderItem = new WorkItem(pictures, templateType, title, desc, creatorUserId, workId);
            _workItems.Add(newOrderItem);

            return (true, "Ok");
        }
    }
}
