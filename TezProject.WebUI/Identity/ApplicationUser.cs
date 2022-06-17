using Microsoft.AspNetCore.Identity;

namespace TezProject.WebUI.Identity
{
    public class ApplicationUser:IdentityUser
    {
        public string FullName { get; set; }
    }
}
