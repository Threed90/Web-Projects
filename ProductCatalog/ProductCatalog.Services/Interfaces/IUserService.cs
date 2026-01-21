using System;
using System.Collections.Generic;
using System.Text;

namespace ProductCatalog.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(string username, string password, string confirmPass);

        Task<bool> Login(string username, string password);

        Task Logout(string? username);

    }
}
