using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using WebApplication1.Models;

[assembly: OwinStartupAttribute(typeof(WebApplication1.Startup))]
namespace WebApplication1
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateDefaultRolesAndUsers();
        }

        public void CreateDefaultRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            IdentityRole role = new IdentityRole();
            if (!roleManager.RoleExists("Deler"))
            {
                role.Name = "Deler";
                roleManager.Create(role);
                ApplicationUser user = new ApplicationUser();
                user.UserName = "Deler";
                user.UserType = "Deler";
                user.Email = "naalkotabahnaal@gmail.com";

                var Check = userManager.Create(user, "Nael@2006");
                if (Check.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Deler");
                }
            }
        }
    }
}
