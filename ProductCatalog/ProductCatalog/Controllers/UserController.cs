using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models.DTO;
using ProductCatalog.Services.Interfaces;

namespace ProductCatalog.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            var model = new LoginDTO()
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDTO loginModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(loginModel);
            }

            var result = await userService.Login(loginModel.Username, loginModel.Password);

            if(result)
            {
                if (!string.IsNullOrEmpty(loginModel.ReturnUrl))
                {
                    return Redirect(loginModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password.");
                return View(loginModel);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterDTO();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDTO registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerModel);
            }
            var result = await userService.Register(registerModel.Username, registerModel.Password, registerModel.ConfirmPassword);
            if (result)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Registration failed. Please check your details and try again.");
                return View(registerModel);
            }
        }

        public async Task<IActionResult> Logout()
        {
            var username = User.Identity?.Name;
            await userService.Logout(username);
            return RedirectToAction("Login", "User");
        }
    }
}
