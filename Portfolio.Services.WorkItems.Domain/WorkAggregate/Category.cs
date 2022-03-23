using Portfolio.Services.WorkItems.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Domain.WorkAggregate
{
    public class Category : Entity, IAggregateRoot
    {
        [MaxLength(150)]
        [Required]
        public string Title { get; set; }
        [MaxLength(200)]
        [Required]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int CreatorUserId { get; set; }

        public short Sort { get; set; }
        public DateTime? CreatedDate { get; private set; }
        public DateTime? UpdatedDate { get; private set; }

        public Category()
        {

        }
        public Category(string title,string description,bool isActive,int creatorUserId)
        {
          
            Title = title;
            Description = description;
            IsActive = isActive;
            CreatorUserId = creatorUserId;
            CreatedDate = DateTime.Now;
        }
    }
}
