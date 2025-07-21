namespace BookManagementAPI.Controllers
{
    using BookManagementAPI.Helpers;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller responsible for handling user authentication.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenGenerator tokenGenerator;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// Constructor that injects the JWT token generator dependency.
        /// </summary>
        /// <param name="tokenGenerator">An implementation of IJwtTokenGenerator used to generate JWT tokens.</param>
        public AuthController(IJwtTokenGenerator tokenGenerator)
        {
            this.tokenGenerator = tokenGenerator;
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
            {
                return this.BadRequest();
            }

            // Simple hardcoded authentication logic for demonstration purposes
            if (request.Username == "admin" && request.Password == "password")
            {
                var token = this.tokenGenerator.GenerateToken(request.Username);
                return this.Ok(new { token });
            }

            return this.Unauthorized();
        }
    }
}
