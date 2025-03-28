using Microsoft.AspNetCore.Identity;

namespace Progetto_BE_S7.Models.Auth
{
    public class ApplicationRole : IdentityRole
    {
        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}
