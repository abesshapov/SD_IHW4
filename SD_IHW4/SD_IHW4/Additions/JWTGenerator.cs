using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SD_IHW4 {
    public record TokenInfo(String token, DateTime expires);
    public static class JWTGenerator {
        const string KEY = "secret_key!frfritis";
        public const int LIFETIME = 10;
        private static SymmetricSecurityKey GetSymmetricSecurityKey() {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        public static TokenInfo Generate() {
            var now = DateTime.Now;
            var expires = now.Add(TimeSpan.FromMinutes(LIFETIME));
            var jwt = new JwtSecurityToken(
                    notBefore: now,
                    expires: expires,
                    signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return new TokenInfo(encodedJwt, expires);
        }
    }
}