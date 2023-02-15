using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoginFPTBook.Constants;
using Microsoft.AspNetCore.Identity;

namespace LoginFPTBook.Data
{
    public static class DbSeeder
    {
         public static async Task SeedRolesAndAdminAsync(IServiceProvider service){

            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager =  service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Owner.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Customer.ToString()));
         }
    }
}