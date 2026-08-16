using Microsoft.AspNetCore.Identity;

namespace Portfolio.Api.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}
