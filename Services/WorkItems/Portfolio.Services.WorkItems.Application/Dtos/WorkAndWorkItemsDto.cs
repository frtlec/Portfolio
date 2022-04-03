using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Dtos
{
    public  class WorkAndWorkItemsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MainPicture { get; set; }
        public short CategoryId { get; set; }
        public bool IsActive { get; set; }

        public List<WorkItemDto> WorkItems { get; set; }
    }
}
