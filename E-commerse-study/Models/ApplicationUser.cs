using Microsoft.AspNetCore.Identity;

namespace E_commerse_study.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Address { get; set; }

    }
}
