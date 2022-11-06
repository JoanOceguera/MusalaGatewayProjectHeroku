using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusalaGatewayProject.Models;
using MusalaGatewayProject.Models.Dto;
using MusalaGatewayProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusalaGatewayProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        protected ResponseDto _response;


        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _response = new ResponseDto();
        }

        // POST: api/Register
        [HttpPost("Register")]
        public async Task<ActionResult> Register(UserDto newUser)
        {
            if (!await _userRepository.UserExists(newUser.UserName))
            {
                try
                {
                    var user = await _userRepository.Register(
                        new User
                        {
                            UserName = newUser.UserName
                        }, newUser.Password);
                    var token = await _userRepository.Login(newUser.UserName, newUser.Password);
                    _response.Result = new JwtDto { UserName = user.UserName, Token = token };
                    _response.DisplayMessage = "Successfully registered user";
                }
                catch (Exception e)
                {

                    _response.IsSuccess = false;
                    _response.DisplayMessage = "An error occurred";
                    _response.ErrorMessages = new List<string> { e.ToString() };
                    return BadRequest(_response);
                }
            }
            else
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "The user already exists";
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        // POST: api/Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login(UserDto user)
        {
            try
            {
                var response = await _userRepository.Login(user.UserName, user.Password);
                if(response.Equals("Wrong password")  || response.Equals("The user not exists"))
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = response;
                    return BadRequest(_response);
                }
                _response.DisplayMessage = "Login successful";
                _response.Result = new JwtDto { UserName = user.UserName, Token = response};
            }
            catch (Exception e)
            {

                _response.IsSuccess = false;
                _response.DisplayMessage = "An error occurred";
                _response.ErrorMessages = new List<string> { e.ToString() };
                return BadRequest(_response);
            }
            return Ok(_response);
        }
    }
}
