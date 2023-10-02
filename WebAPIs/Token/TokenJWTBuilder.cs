using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebAPIs.Token
{
    public class TokenJWTBuilder
    {
        private SecurityKey securitKey = null;
        private string subject = "";
        private string issuer = "";
        private string audience = "";
        private Dictionary<string, string> claims = new Dictionary<string, string>();
        private int expireInMinutes = 5;

        public TokenJWTBuilder AddSecutiryKey(SecurityKey securitKey)
        {
            this.securitKey = securitKey;
            return this;
        }

        public TokenJWTBuilder AddSubject(string subject)
        {
            this.subject = subject;
            return this;
        }

        public TokenJWTBuilder AddIssuer(string issuer)
        {
            this.issuer = issuer;
            return this;
        }

        public TokenJWTBuilder AddClaim(string type, string value)
        {
            this.claims.Add(type, value);
            return this;
        }

        private void EnsureArguments()
        {
            if (this.securitKey == null)
                throw new ArgumentNullException("SecurityKey");

            if (string.IsNullOrEmpty(this.subject))
                throw new ArgumentNullException("subject");

            if (string.IsNullOrEmpty(this.issuer))
                throw new ArgumentNullException("issuer");

            if (string.IsNullOrEmpty(this.audience))
                throw new ArgumentNullException("audience");

        }

        public TokenJWT Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, this.subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }.Union(this.claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: this.issuer,
                audience: this.audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireInMinutes),
                signingCredentials: new SigningCredentials(this.securitKey, SecurityAlgorithms.HmacSha256)
                );

            return new TokenJWT(token);
        }
    }
}

 
