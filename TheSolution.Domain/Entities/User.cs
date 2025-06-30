using Microsoft.AspNetCore.Identity;

namespace TheSolution.Domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Pathronomic { get; set; }
    }
}
