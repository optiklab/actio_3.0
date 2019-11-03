namespace Actio.Common.Auth
{
    public class JwtOptions
    {
        public string SecretKey { get; set; }
        public int ExpiryMinutes { get; set; }
        /// <summary>
        /// Name of the service responsible for creating tokens.
        /// </summary>
        /// <value></value>
        public string Issuer { get; set; }
    }
}