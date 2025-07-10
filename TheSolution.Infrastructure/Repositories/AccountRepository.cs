using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TheSolution.Domain.Entities;
using TheSolution.Domain.Interfaces;

namespace TheSolution.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> um;
        private readonly SignInManager<User> sim;
        private readonly ILogger<AccountRepository> logger;
        public AccountRepository(UserManager<User> _um, SignInManager<User> _sim, ILogger<AccountRepository> _logger)
        {
            um = _um;
            sim = _sim;
            logger = _logger;
        }
        public async Task<User> Login(User user, string password)
        {
            if(user == null)
            {
                logger.LogError("Передан пустой польщователь");
                return null;
            }
            else
            {
                User? loginUser = await um.FindByEmailAsync(user.Email);
                if(loginUser == null)
                {
                    loginUser = await um.FindByNameAsync(user.Name);
                    if(loginUser == null)
                    {
                        logger.LogError("Пользователь не найден");
                        return null;
                    }
                }
                if(loginUser != null)
                {
                    bool passwordValue = await um.CheckPasswordAsync(loginUser, password);
                    if(!passwordValue)
                    {
                        logger.LogError("Передан неверный пароль");
                        return null;
                    }
                    logger.LogInformation("Авторизация успешна");
                    return loginUser;
                }
                else
                {
                    logger.LogError("Пользователя не существует");
                    return null;
                }

            }
        }

        public async Task<User> Register(User user, string password)
        {
            if(user == null)
            {
                logger.LogError("Передан пустой пользователь");
            }
            if(await um.FindByEmailAsync(user.Email) != null)
            {
                logger.LogError("Существует");
            }
            if(await um.FindByNameAsync(user.Name) != null)
            {
                logger.LogError("Существует");
            }
            User regUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
                Pathronomic = user.Pathronomic
            };
            IdentityResult registerResult = await um.CreateAsync(regUser, password);
            if(!registerResult.Succeeded)
            {
                logger.LogError("Ошибка регистрации пользователя");
                foreach(var error in registerResult.Errors)
                {
                    logger.LogError(error.Description.ToString());
                }
                return null;
            }
            logger.LogInformation("Регистрация прошла успешно");
            IdentityResult roleResult = await um.AddToRoleAsync(regUser, "User");
            if (roleResult.Succeeded)
            {
                logger.LogInformation("Пользователь зарегистрирован с ролью User");
            }
            else
            {
                logger.LogError("Не удалось создать пользователя");
            }
            return regUser;
        }

        public async Task SignInAsyn(User user, bool ispersistent)
        {
            if(user == null)
            {
                logger.LogError("В репозиторий передан пустой пользователь");
                return;
            }
            try
            {
                await sim.SignInAsync(user, isPersistent: ispersistent);
                logger.LogInformation("Произведена попытка авторизации");
            }
            catch (Exception ex)
            {
                logger.LogError("Ошибка метода SignInAsync в репозитории");
            }
        }
    }
}
