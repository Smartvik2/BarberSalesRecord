using BarberSalesRecord.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberSalesRecord.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateToken(
            ApplicationUser user,
            IList<string> roles,
            IConfiguration config)
        {
            // 1) Read settings
            var secretKey = config["JwtSettings:SecretKey"]!;
            var issuer = config["JwtSettings:Issuer"]!;
            var audience = config["JwtSettings:Audience"]!;
            var minutes = int.Parse(config["JwtSettings:DurationInMinutes"]!);

            // 2) Build claims
            var now = DateTime.UtcNow;

            // after var now = DateTime.UtcNow;
            var secondsSinceEpoch = new DateTimeOffset(now).ToUnixTimeSeconds();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    secondsSinceEpoch.ToString(),
                    ClaimValueTypes.Integer64),
                    new Claim("name", user.Name ?? "")
                        // plus role claims...
            };


            // role claims
            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            // 3) Create signing key & credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4) Create token
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(minutes),
                signingCredentials: creds);

            // 5) Return serialized JWT
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
