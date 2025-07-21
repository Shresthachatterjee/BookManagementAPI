namespace BookManagementAPI.Helpers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    /// <summary>
    /// This class is responsible for generating JWT tokens.
    /// It retrieves JWT settings from configuration and creates signed tokens containing user claims.
    /// </summary>
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration config;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtTokenGenerator"/> class.
        /// Constructor that receives application configuration through dependency injection.
        /// This configuration is used to read JWT settings like secret key, issuer, audience, and expiration time.
        /// </summary>
        /// <param name="config">Application configuration containing JWT settings.</param>
        public JwtTokenGenerator(IConfiguration config)
        {
            this.config = config;
        }

        /// <summary>
        /// Generates a signed JWT token for a given username.
        /// The token includes claims and expiration, and is signed using a symmetric security key.
        /// </summary>
        /// <param name="username">The username to include in the token claims.</param>
        /// <returns>A JWT token string that can be used for authentication.</returns>
        public string GenerateToken(string username)
        {
            // Retrieve JWT settings from appsettings.json
            var jwtSettings = config.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expiryMinutes = Convert.ToInt32(jwtSettings["ExpiryMinutes"]);

            // Create a symmetric security key from the secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Generate signing credentials using HMAC SHA256 algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create an array of claims that represent the user identity
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Create the JWT token with issuer, audience, claims, expiration, and signing credentials
            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: credentials);

            // Serialize the token to a string and return it
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
