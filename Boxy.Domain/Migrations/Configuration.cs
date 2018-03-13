namespace Boxy.Domain.Migrations
{
    using Boxy;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Boxy.Domain.Entities;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var category = new List<Category>()
            {
                new Category(){Name = "Без категории"}
            };
            context.Categories.AddRange(category);

            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "client" };

           
            roleManager.Create(role1);
            roleManager.Create(role2);
            

            var admin = new ApplicationUser { Email = "admin@mail.ru", UserName = "admin@mail.ru" };
            string password = "Battlefield_4";
            var resultAdmin = userManager.Create(admin, password);


            var client = new ApplicationUser { Email = "client@mail.ru", UserName = "client@mail.ru" };
            string passwordClient = "Battlefield_4";
            var resultClient = userManager.Create(client, passwordClient);


            if (resultAdmin.Succeeded && resultClient.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(client.Id, role2.Name);
            }


        }
    }
}
