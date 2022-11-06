using MusalaGatewayProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusalaGatewayProject.Repository
{
    public interface IUserRepository
    {
        Task<User> Register(User user, string password);

        Task<string> Login(string userName, string password);

        Task<bool> UserExists(string userName);
    }
}
