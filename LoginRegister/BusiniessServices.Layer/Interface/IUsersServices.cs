using BusiniessServices.Layer.DTO;
using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusiniessServices.Layer.Interface
{
    public interface IUsersServices
    {
        Task<Response<string>> Register(UsersDTO users);
        Task<Response<string>> Login(loginDTO users);
        Task<Response<string>> ChangePassword(string token,PasswordDTO passwordDTO);
        Task<Response<string>> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<Response<string>> ResetPassword(string token,ResetPasswordDTO resetPasswordDTO);
    }
}
