using AutoMapper;
using Portfolio.Services.WorkItems.Application.Dtos;
using Portfolio.Services.WorkItems.Domain.WorkAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Mapping
{
    public class CustomMapping: Profile
    {
        public CustomMapping()
        {
            CreateMap<Work, WorkDto>().ReverseMap();
            CreateMap<WorkItem, WorkItemDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
