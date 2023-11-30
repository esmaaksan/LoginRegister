using BusiniessServices.Layer.Interface;
using BusiniessServices.Layer.Services;
using DataAccess.Layer.AddressesValidation;
using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using DataAccess.Layer.Security;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;

namespace LoginRegister.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUsersServices _usersservices;

        public UserController(IUsersServices usersservices)
        {
            _usersservices = usersservices;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UsersDTO user)
        {
            UsersValidators validationRules = new UsersValidators();
            ValidationResult validationResult = validationRules.Validate(user);
            
            if(validationResult.IsValid )
            {
                var veri = await _usersservices.Register(user);
                return Ok(veri);
            }
            else
            {

                foreach (var fail in validationResult.Errors)
                {
                    return Ok(fail.ErrorMessage);
                }
            }
            return Ok(ModelState);
           
            
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(loginDTO user)
        {
            var veri = await _usersservices.Login(user);
            return Ok(veri);
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var veri = await _usersservices.ForgotPassword(forgotPasswordDTO);
            return Ok(veri);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var veri = await _usersservices.ResetPassword(userEmail,resetPasswordDTO);
            return Ok(veri);
        }
        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(PasswordDTO passwordDTO)
        {
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _usersservices.ChangePassword(userEmail, passwordDTO);
            return Ok(result);
        }
    }
}
