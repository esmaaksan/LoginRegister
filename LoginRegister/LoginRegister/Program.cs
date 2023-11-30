
using BusiniessServices.Layer.CustomValidations;
using BusiniessServices.Layer.Interface;
using BusiniessServices.Layer.Services;
using DataAccess.Layer.AddressesValidation;
using DataAccess.Layer.Concrete;
using DataAccess.Layer.Context;
using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using DataAccess.Layer.Helpers.EMapper;
using DataAccess.Layer.Interface;
using DataAccess.Layer.Security;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace LoginRegister
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer
                (options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Token:Issuer"],
                        ValidAudience = builder.Configuration["Token:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
                        ClockSkew = TimeSpan.Zero
                    };
                }
                );
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
                   
            });

            builder.Services.AddIdentityCore<User>(options =>
            {
                
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            // Add services to the container.

            builder.Services.AddDbContext<AppDbContext>();
            // Add services to the container.

            builder.Services.AddSingleton<IValidator<Adresses>, AddressesValidators>();
            builder.Services.AddSingleton<IValidator<UsersDTO>, UsersValidators>();
            builder.Services.AddControllers();
            


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //authorize ayarlarý
            builder.Services.AddSwaggerGen(option=>
            {
                option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "LoginRegister", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "lütfen token deðeri girin",
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference= new OpenApiReference
                        {
                            Type= ReferenceType.SecurityScheme,
                            Id= "Bearer"
                        }
                    },
                    new string[]{}
                    }
                });
            });
            builder.Services.AddAutoMapper(typeof(Mapper).Assembly);
            builder.Services.AddScoped<IAddressesRepository, AddressesRepository>();
            builder.Services.AddScoped<IAddressesServices, AdressesServices>();
            builder.Services.AddScoped<IUsersServices, UsersServices>();
            builder.Services.AddScoped<IJWT, DataAccess.Layer.Security.TokenHandler>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}