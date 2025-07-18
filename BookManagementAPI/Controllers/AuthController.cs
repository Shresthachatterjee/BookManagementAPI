using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.Helpers;

namespace BookManagementAPI.Controllers
{
    /// <summary>
    /// Controller responsible for handling user authentication.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenGenerator _tokenGenerator;

        /// <summary>
        /// Constructor that injects the JWT token generator dependency.
        /// </summary>
        /// <param name="tokenGenerator">An implementation of IJwtTokenGenerator used to generate JWT tokens.</param>
        public AuthController(IJwtTokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }

        /// <summary>
        /// Authenticates the user based on the provided credentials.
        /// Returns a JWT token if authentication is successful.
        /// </summary>
        /// <param name="request">Login credentials including username and password.</param>
        /// <returns>HTTP 200 with token if successful, 400 if request is null, 401 if credentials are invalid.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request == null)
                return BadRequest();

            // Simple hardcoded authentication logic for demonstration purposes
            if (request.Username == "admin" && request.Password == "password")
            {
                var token = _tokenGenerator.GenerateToken(request.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }

    /// <summary>
    /// Model representing the login request payload.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// The username of the user attempting to log in.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password of the user attempting to log in.
        /// </summary>
        public string Password { get; set; }
    }
}
