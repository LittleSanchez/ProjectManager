using Microsoft.AspNetCore.Identity;
using ExpBag.Application.Constans;
using ExpBag.Domain;
using ExpBag.EFData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Infrastructure.Extentions
{
    public static class EFDataContextExtension
    {
        public static void EnsureSeeder(this DataContext context,
            UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            CreateDefaultRoles(roleManager);
            CreateDefaultUser(userManager);
        }
        private static void CreateDefaultRoles(RoleManager<AppRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                var result = roleManager.CreateAsync(new AppRole
                {
                    Name = Roles.Admin
                }).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Defalut role {Roles.Admin} cannot be created");
                }
                result = roleManager.CreateAsync(new AppRole
                {
                    Name = Roles.User
                }).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException($"Defalut role `{Roles.User}` cannot be created");
                }
            }
        }
        private static void CreateDefaultUser(UserManager<AppUser> userManager)
        {
            /*var email = "admin@gmail.com";

            var findUser = userManager.FindByEmailAsync(email).Result;
            if (findUser == null)
            {
                var user = new AppUser
                {
                    DisplayName = "admin",
                    UserName = email,
                    Email = email,
                    PhoneNumber = "380976785643"
                };
                var result = userManager.CreateAsync(user, "Qwerty1-").Result;

                result = userManager.AddToRoleAsync(user, Roles.Admin).Result;
            }*/
            if (!userManager.Users.Any())
            {
                AppUser admin = new AppUser()
                {
                    DisplayName = "Admin",
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    PhoneNumber = "+380950000000"
                };
                var res = userManager.CreateAsync(admin, "Qwerty1-").Result;
                if (!res.Succeeded)
                {
                    var exception = new ApplicationException($"Default user `{admin.Email}` cannot be created");
                    throw exception;
                }

                res = userManager.AddToRoleAsync(admin, Roles.Admin).Result;

                AppUser user = new AppUser()
                {
                    DisplayName = "User",
                    UserName = "user@gmail.com",
                    Email = "user@gmail.com",
                    PhoneNumber = "+38095000000"
                };
                res = userManager.CreateAsync(user, "Qwerty1+").Result;
                if (!res.Succeeded)
                {
                    var exception = new ApplicationException($"Default user `{user.Email}` cannot be created");
                    throw exception;
                }

                res = userManager.AddToRoleAsync(user, Roles.User).Result;
            }
        }
    }
}
