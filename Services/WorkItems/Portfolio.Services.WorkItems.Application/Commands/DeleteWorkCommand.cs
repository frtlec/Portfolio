using MediatR;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Commands
{
    public class DeleteWorkCommand:IRequest<Response<DeleteWorkDto>>
    {
        public int WorkId { get; set; }
    }
}
