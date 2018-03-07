using Company.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.Data
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@mail.ru";
            string password = "Qwerty12-";
            string managerEmail = "manager@mail.ru";
            string managerPassword = "Qwerty12-";
            string accounterEmail = "accounter@mail.ru";
            string accounterPassword = "Qwerty12-";
            if (await roleManager.FindByNameAsync("Администратор") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Администратор"));
            }
            if (await roleManager.FindByNameAsync("Менеджер") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Менеджер"));
            }
            if (await roleManager.FindByNameAsync("Учётчик") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Учётчик"));
            }

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Администратор");
                    await userManager.AddToRoleAsync(admin, "Менеджер");
                    await userManager.AddToRoleAsync(admin, "Учётчик");
                }
            }
            if(await userManager.FindByNameAsync(managerEmail) == null)
            {

                User manager = new User { Email = managerEmail, UserName = managerEmail };
                IdentityResult resultManager = await userManager.CreateAsync(manager, managerPassword);
                if (resultManager.Succeeded)
                {
                    await userManager.AddToRoleAsync(manager, "Менеджер");
                    await userManager.AddToRoleAsync(manager, "Учётчик");

                }
            }
            if(await userManager.FindByNameAsync(accounterEmail) == null)
            {
                User accounter = new User { Email = accounterEmail, UserName = accounterEmail };
                IdentityResult resultAccounter = await userManager.CreateAsync(accounter, accounterPassword);
                if (resultAccounter.Succeeded)
                {
                    await userManager.AddToRoleAsync(accounter, "Учётчик");
                }
            }
        }
    }
}
