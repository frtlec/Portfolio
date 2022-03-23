using MediatR;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Domain.WorkAggregate;
using Portfolio.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Commands
{
    public class CreateWorkCommand : IRequest<Response<CreatedWorkDto>>
    {
        public string MainPicture { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public short CategoryId { get; set; }
        public List<WorkItemDto> WorkItems { get; set; }
    }
}
