using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Services.WorkItems.Application.Constants.ResponseMessages
{
    public class ValidatorResponseMessage
    {
        public const string CANNOT_EMPTY_OR_NULL = "value cannot be empty or null"; 
        public const string OOPS_SWAGGER_STRING = "Hey! this swagger string value"; 
        public const string MAX_LENGTH_EXCEEDED = "max character length exceeded"; 
    }
}
