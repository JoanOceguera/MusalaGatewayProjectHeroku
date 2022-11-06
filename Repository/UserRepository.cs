using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MusalaGatewayProject.Data;
using MusalaGatewayProject.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusalaGatewayProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> Login(string userName, string password)
        {
            if (await UserExists(userName))
            {
                var user = _context.Users.FirstOrDefault(x => x.UserName == userName);
                if(!VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash))
                {
                    return "Wrong password";
                }
                else
                {
                    return CreateJWToken(user);
                }
            }
            else
            {
                return "The user not exists";
            }
        }

        public async Task<User> Register(User user, string password)
        {

            CreatePasswordHash(password, out byte[] passwordSalt, out byte[] passwordHash);

            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _context.Users.AnyAsync(x=> x.UserName.ToLower().Equals(userName.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte [] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }                        
                }
                return true;
            }
        }

        private string CreateJWToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = System.DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
