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
    public class CreateCategoryCommand : IRequest<Response<CreateCategoryDto>>
    { 
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public short Sort { get; set; }
    }
}
