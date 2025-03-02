using Microsoft.AspNetCore.Identity;

namespace WeddingPlatform.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<WeddingSite> WeddingSites { get; set; }
    }
}