using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Actio.Common.Auth
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SecurityKey _issuerSigningKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(IOptions<JwtOptions> options)
        {
            _options = options.Value;

            _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            // !!! NOTE
            // Another BETTER option would be to use RSA here, because in case of HMAC we need to have same secret key
            // shared between Identity service and other services like API.
            // In case of RSA Identity service would have the private/public certificates - private key and public key.
            // Then it could provide the public key for all of the other services - they only have to verify if token is valid, which
            // could be done via public key. Private key needs to be used only inside of the Identity service scope, for the singing tokens.

            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);

            _jwtHeader = new JwtHeader(_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, // Don't care which end client will be authenticated
                ValidIssuer = _options.Issuer,
                IssuerSigningKey = _issuerSigningKey
            };
        }

        public JsonWebToken Create(Guid userId)
        {
            var nowUtc = DateTime.UtcNow;
            var expires = nowUtc.AddMinutes(_options.ExpiryMinutes);
            var centuryBegin = new DateTime(1970, 1, 1).ToUniversalTime();
            var exp = (long)(new TimeSpan(expires.Ticks - centuryBegin.Ticks).TotalSeconds);
            var now = (long)(new TimeSpan(nowUtc.Ticks - centuryBegin.Ticks).TotalSeconds);
            var payload = new JwtPayload
            {
                {"sub", userId}, // subject
                {"iss", _options.Issuer},
                {"iat", now}, // issued at
                {"exp", exp}, // experiation date
                {"unique_name", userId} // user within API
            };
            var jwt = new JwtSecurityToken(_jwtHeader, payload);
            var token = _jwtSecurityTokenHandler.WriteToken(jwt);

            return new JsonWebToken
            {
                Token = token,
                Expires = exp
            };
        }
    }
}