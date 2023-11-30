
using BusiniessServices.Layer.DTO;
using BusiniessServices.Layer.ErrorMessages;
using BusiniessServices.Layer.Interface;
using DataAccess.Layer.AddressesValidation;
using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using DataAccess.Layer.Interface;
using DataAccess.Layer.Security;
using FluentValidation;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusiniessServices.Layer.Services
{
    public class UsersServices : IUsersServices
    {
        private readonly UserManager<User> _userManager;
        private readonly IJWT _jwt;
        
        public UsersServices(UserManager<User> userManager,IJWT jwt)
        {
            _userManager = userManager;
            _jwt = jwt;
           
            
        }

        public async Task<Response<string>> ChangePassword(string token,PasswordDTO passwordDTO)
        {
            
            User? user= await _userManager.FindByEmailAsync(token);//tokenin içindeki claimden gelen mail
            if(passwordDTO.CurrentPassword!= passwordDTO.NewPassword)
            {
                if(passwordDTO.NewPassword==passwordDTO.ConfirmPassword)
                {
                    var result = await _userManager.ChangePasswordAsync(user, passwordDTO.CurrentPassword, passwordDTO.NewPassword);
                    if(!result.Succeeded)
                    {
                        return Response<string>.Fail(Message.OldPasswordInCorrect);
                    }
                    await _userManager.UpdateAsync(user);
                    return Response<string>.Succes();
                }
                
            }
            return Response<string>.Fail(Message.PasswordInCorrect);
            
        }

        public async Task<Response<string>> ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            
            User user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);
            Random random= new Random();
            user.ConfirmationCode = random.Next(1000, 9999);
            if (user != null)
            {
                var jwt= _jwt.CreateToken(user);

                MailMessage mail = new MailMessage();               
                mail.To.Add(user.Email);
                mail.From = new MailAddress(forgotPasswordDTO.Email, "Şifre Güncelleme", System.Text.Encoding.UTF8);
                mail.Subject = "Şifre Güncelleme Talebi";
                mail.Body= $"Doğrulama kodunuz: {user.ConfirmationCode}";
            
                SmtpClient smp = new SmtpClient();
                smp.Credentials = new NetworkCredential("e16184884@gmail.com", "uule aypn ojwc zhon");
                smp.Port = 587;
                smp.Host = "smtp.gmail.com";
                smp.EnableSsl = true;
                await smp.SendMailAsync(mail);
                await _userManager.UpdateAsync(user);
                
                
                
                return Response<string>.Succes(jwt);
            }
            return Response<string>.Fail();
        }

        public async Task<Response<string>> Login(loginDTO users)
        {
           TokenModel tokenModel = new TokenModel();   
           var user= await _userManager.FindByEmailAsync(users.Email);
           var result = await _userManager.CheckPasswordAsync(user, users.Password);
            if (user != null && result==true)
            {
              tokenModel.token= _jwt.CreateToken(user);
              var e = tokenModel.token;
              return Response<string>.Succes(e);
                
            }
            else
            {
                return Response<string>.Fail(Message.PasswordInCorrect);
            }

            
        }

        public async Task<Response<string>> Register(UsersDTO users)
        {
           
            var email = await _userManager.FindByEmailAsync(users.Email);
            if(email != null)
            {
               return Response<string>.Fail(Message.UserAlreadyExists);
                
            }
            User users1 = new User()
            {
                UserName = users.UserName,
                Email = users.Email,
                
            };

            IdentityResult result = await _userManager.CreateAsync(users1, users.Password);
            if (!result.Succeeded)
            {
                return Response<string>.Fail();
            }
            return Response<string>.Succes();
            
        
        }

        public async Task<Response<string>> ResetPassword(string token,ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(token);

            if(user.ConfirmationCode==resetPasswordDTO.ConfirmationCode)
            {
                var result = await _userManager.RemovePasswordAsync(user);
                var result1 = await _userManager.AddPasswordAsync(user,resetPasswordDTO.NewPassword);
                if (result1.Succeeded)
                {
                    await _userManager.UpdateAsync(user);
                    return Response<string>.Succes();
                }
            }
            return Response<string>.Fail(); 
        }
    }
}
