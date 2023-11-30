using DataAccess.Layer.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Security
{
    public class TokenHandler:IJWT
    {
       
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(User user)
        {
            //Claim claim = null;
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,user.Email),//ulaşılabilir verileri içinde tutabilirsin
                //new Claim(JwtRegisteredClaimNames.NameId,user.Id)
                new Claim("Id",user.Id)
            };
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey
               (Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            JwtSecurityToken jwtSecurityToken = new(
                claims: claims,
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: DateTime.Now.AddDays(Convert.ToInt16(2)),
                signingCredentials: new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256));//dijital imza oluşturmak için (son kısım)
            
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        }
    }
}
