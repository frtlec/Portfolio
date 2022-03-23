using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Dtos
{
    public class CreatedWorkDto
    {
        public int WorkId { get; set; }
        public Dictionary<string,string> HasIsBeenAdded { get; set; }

    }

}
