namespace WIT.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WIT.Models.EntityModels;

    internal sealed class Configuration : DbMigrationsConfiguration<WitContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(WitContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            //add roles
            if (!context.Roles.Any(role => role.Name == "Admin"))
            {
                var role = new IdentityRole("Admin");
                manager.Create(role);
            }

            if (!context.Roles.Any(role => role.Name == "Moderator"))
            {
                var role = new IdentityRole("Moderator");
                manager.Create(role);
            }


            if (!context.Roles.Any(role => role.Name == "User"))
            {
                var role = new IdentityRole("User");
                manager.Create(role);
            }

            //default admin user
            if (!(context.Users.Any(u => u.UserName == "iskrenpenew@gmail.com")))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var user = new User
                {
                    UserName = "iskrenpenew@gmail.com",
                    Email = "iskrenpenew@gmail.com",
                    CompanyName = "Freelance Ltd."
                };
                var result = userManager.Create(user, "asdasd1");
                if (result.Succeeded)
                {
                    userManager.AddToRoles(user.Id, "Admin", "User");
                }
            }
        }
    }
}
