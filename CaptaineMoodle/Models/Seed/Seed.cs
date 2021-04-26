using CaptaineMoodle.Areas.Identity.Data;
using CaptaineMoodle.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CaptaineMoodle.Models
{
    public static class Seed
    {
        public static void SeedData (UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers (UserManager<User> userManager)
        {

            if (userManager.FindByNameAsync("admin@test").Result == null)
            {
                User user = new User();
                user.UserName = "admin@test";
                user.Email = "admin@test";

                IdentityResult result = userManager.CreateAsync
                (user, "Admin.1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.FindByNameAsync("teacher@test").Result == null)
            {
                User user = new User();
                user.UserName = "teacher@test";
                user.Email = "teacher@test";

                IdentityResult result = userManager.CreateAsync
                (user, "Teacher.1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Teacher").Wait();
                }
            }

            if (userManager.FindByNameAsync("student@test").Result == null)
            {
                User user = new User();
                user.UserName = "student@test";
                user.Email = "student@test";

                IdentityResult result = userManager.CreateAsync
                (user, "Student.1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Student").Wait();
                }
            }
        }

        public static void SeedRoles (RoleManager<IdentityRole> roleManager)
        {

            if (!roleManager.RoleExistsAsync ("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Teacher").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Teacher";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Student";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
