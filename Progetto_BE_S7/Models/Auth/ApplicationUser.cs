using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Progetto_BE_S7.Models.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public required string FirstName { get; set; }
        [Required]
        public required string LastName { get; set; }

        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}
