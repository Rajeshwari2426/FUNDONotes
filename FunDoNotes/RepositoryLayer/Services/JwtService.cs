using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    //public class JwtService
    //{
    //    private readonly string _secret;
    //    private readonly string _expDate;

    //    public JwtService(IConfiguration config)
    //    {
    //        _secret = config.GetSection("JwtConfig").GetSection("secret").Value;
    //        _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value;

    //    }
    //    public string GenerateSecurityToken(string email, long userId)
    //    {
    //        var tokenHandler = new JwtSecurityTokenHandler();
    //        var key = Encoding.ASCII.GetBytes(_secret);
    //        var tokenDescriptor = new SecurityTokenDescriptor
    //        {
    //            Subject = new ClaimsIdentity(new[]
    //            {
    //                new Claim(ClaimTypes.Email, email),
    //                new Claim("UserId", userId.ToString()),
    //            }),
    //            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_expDate)),
    //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    //        };
    //        var token = tokenHandler.CreateToken(tokenDescriptor);
    //        return tokenHandler.WriteToken(token);
    //    }
    //}
}
