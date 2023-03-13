using Airbnb.Application.Common.Options;
using Airbnb.Application.Interfaces;
using Airbnb.Domain.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }


        public string Generate(User user, IEnumerable<Role> userRoles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            };
            var roleClaims = userRoles.Select(r => new Claim(ClaimTypes.Role, r.EnglishName));
            claims.AddRange(roleClaims);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SigningSecretKey)),
                SecurityAlgorithms.HmacSha256);

            var encryptingCredentials = new EncryptingCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.EncryptionSecretKey)),
                SecurityAlgorithms.Aes128KW,
                SecurityAlgorithms.Aes128CbcHmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                new ClaimsIdentity(claims),
                null,
                DateTime.UtcNow.AddDays(1),
                DateTime.UtcNow,
                signingCredentials,
                encryptingCredentials
                );

            string tokenValue = tokenHandler.WriteToken(token);
            return tokenValue;
        }

        public string GenerateRandomToken()
        {
            var key = new byte[64];
            RNGCryptoServiceProvider.Create().GetBytes(key);
            var base64Secret = Convert.ToBase64String(key);
            // make safe for url
            var urlEncoded = base64Secret.TrimEnd('=').Replace('+', '-').Replace('/', '_');

            return urlEncoded; 
        }
    }
}
