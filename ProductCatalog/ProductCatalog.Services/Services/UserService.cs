using Microsoft.AspNetCore.Identity;
using ProductCatalog.Models.DB_Models;
using ProductCatalog.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Services.Services
{
    public class UserService : IUserService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserService(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<bool> Login(string username, string password)
        {
            var result = await this.signInManager.PasswordSignInAsync(username, password, false, false);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task Logout(string? username)
        {
            var user = await this.userManager.FindByNameAsync(username);
            if (user != null)
            {
                await this.signInManager.SignOutAsync();
            }
            
        }

        public async Task<bool> Register(string username, string password, string confirmPass)
        {
            if (password != confirmPass)
            {
                return false;
            }

            var user = await this.userManager.FindByNameAsync(username);
            if (user != null)
            {
                return false;
            }

            var result = await this.userManager.CreateAsync(new ApplicationUser
            {
                UserName = username,
                Email = username
            }, password);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
