using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheSolution.Application.DTO;
using TheSolution.Application.Interfaces;
using TheSolution.Domain.Entities;
using TheSolution.Web.Models;

namespace TheSolution.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly IAccountService service;
        private readonly UserManager<User> um;
        private readonly SignInManager<User> sim;
        public AccountController(ILogger<AccountController> _logger, IAccountService _service, UserManager<User> _um, SignInManager<User> _sim) 
        {
            logger = _logger;
            service = _service;

            um = _um;
            sim = _sim;
        }
        
        public IActionResult Login()
        {
            LoginViewModel vm = new LoginViewModel();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //var exUser = await um.FindByNameAsync(vm.UserName); //
                //if(exUser == null) //
                //{ //
                //    exUser = await um.FindByEmailAsync(vm.UserName); //
                //} //
                //var result = await sim.PasswordSignInAsync(exUser, vm.Password, true, false);
                UserDTO userDTO = new UserDTO
                {

                };
                bool result = await service.Login(userDTO, vm.Password);
                if (result == true) 
                {
                    //Придумать что-то сюда на true т.к. сервис возвращает bool
                    HttpContext.Session.SetString("UserID", exUser.Id);
                    var role = await um.GetRolesAsync(exUser);
                    HttpContext.Session.SetString("UserRole", role.FirstOrDefault());
                    await sim.SignInAsync(exUser, isPersistent: false);

                    return RedirectToAction("Index", "");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login attemp");
                }
            }
            return View(vm);
        }
    }
}
