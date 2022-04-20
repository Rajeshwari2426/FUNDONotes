using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRl : IUserRl
    {
        private readonly FunDoContext funDoContext;
        private readonly IConfiguration configuration;  
        public UserRl(FunDoContext funDoContext, IConfiguration configuration)
        {
            this.funDoContext = funDoContext;
            this.configuration= configuration;
        }
        
        public UserEntity Register(UserRegistration userRegistration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistration.FirstName;
                userEntity.LastName = userRegistration.LastName;
                userEntity.Email = userRegistration.Email;
                userEntity.Password = userRegistration.Password;
                funDoContext.UserTable.Add(userEntity);
                int result = funDoContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public  string Login(UserLogin userLogin)
        {
            try
            {

                var result = funDoContext.UserTable.Where(x => x.Email == userLogin.Email && x.Password == userLogin.Password).FirstOrDefault();
                if (result != null)
                    return GenerateSecurityToken(result.Email,result.UserId);
                else
                    return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GenerateSecurityToken(string email, long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["JwtConfig:secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", userId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public string ForgetPassword(string Email)
        {
            try
            {
                var result=funDoContext.UserTable.FirstOrDefault(x => x.Email == Email);
                if (result != null)
                {
                    var token = GenerateSecurityToken(result.Email, result.UserId);
                    new MSMQ().SendData2Queue(token);
                    return token;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string Reset_Password(ResetPassword resetPassword, string emailId) 
        {
            try
            {
                var result = funDoContext.UserTable.FirstOrDefault(x => x.Email == emailId);

                if (resetPassword.NewPassword == resetPassword.ConfirmPassword)
                {
                    // UserEntity.Password = resetPassword.NewPassword;
                    result.Password = resetPassword.NewPassword;
                    funDoContext.SaveChanges();
                }
                else
                {
                    return "passwords not matching";
                }
                return "password";
               
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

}
