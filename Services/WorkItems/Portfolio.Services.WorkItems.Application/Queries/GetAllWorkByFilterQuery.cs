using MediatR;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Queries
{
    public class GetAllWorkByFilterQuery : IRequest<Response<List<WorkDto>>>
    {
        public int Limit { get; set; }
        public string Search { get; set; }
        public short CategoryId { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
