using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace i_think_so.Application.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<SecurityOptions> _options;

        public TokenService(IOptions<SecurityOptions> options)
        {
            _options = options;
        }

        public string GenerateToken(string username)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username)
                }),
                Issuer = _options.Value.Issuer,
                Audience = _options.Value.Audience,
                Expires = DateTime.UtcNow.AddHours(_options.Value.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Secret)), 
                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
