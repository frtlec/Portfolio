using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Mapping
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() => {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            }, NullLoggerFactory.Instance);
            return config.CreateMapper();
        });
        public static IMapper Mapper => lazy.Value;
    }
}
