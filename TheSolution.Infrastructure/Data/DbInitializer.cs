using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheSolution.Domain.Entities;

namespace TheSolution.Infrastructure.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var logger = serviceProvider.GetRequiredService<ILogger<DbInitializer>>();

            string[] roles = { "Admin", "User" };
            foreach(var role in roles)
            {
                if(!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    logger.LogInformation($"Роль {role} успешно создана");
                }
                else
                {
                    logger.LogError("Ошибка во время создания роли/роли ранее созданы");
                }
            }

            string adminEmail = "Admin@mail.ru";
            string adminPassword = "@dminPassword667";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if(adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    Name = "Admin",
                    Surname = "Admin",
                    Pathronomic = "Admin",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    var addToRoleResult = await userManager.AddToRoleAsync(adminUser, "Admin");
                    if (addToRoleResult.Succeeded)
                    {
                        logger.LogInformation($"Пользователь {adminEmail} успешно создан и назначена роль Admin");
                    }
                    else
                    {
                        logger.LogError($"Не удалось назначить роль Admin пользователю {adminEmail}. Ошибки {string.Join(",",addToRoleResult.Errors)}");
                    }
                }
                else
                {
                    logger.LogError($"Не удалось создать пользователя {adminEmail}. Ошибки {string.Join(",",result.Errors)}");
                }
            }
            else
            {
                logger.LogError($"Пользователь с Email {adminEmail} уже существует");
            }
        }
    }
}
