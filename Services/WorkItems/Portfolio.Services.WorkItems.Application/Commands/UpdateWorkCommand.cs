using MediatR;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Shared.Dtos;
using System.Collections.Generic;

namespace Portfolio.Services.WorkItems.Application.Commands
{
    public class UpdateWorkCommand : IRequest<Response<UpdateWorkDto>>
    {
        public int WorkId { get; set; }
        public string MainPicture { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public short CategoryId { get; set; }
        public List<WorkItemUpdateDto> WorkItems { get; set; }
    }
}
