// <copyright file="AuthControllerTest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable

namespace BookManagementAPI.Tests
{
    using BookManagementAPI.Controllers;
    using BookManagementAPI.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit tests for the AuthController to validate authentication behavior.
    /// </summary>
    public class AuthControllerTests
    {
        private readonly Mock<IJwtTokenGenerator> mockTokenGenerator;
        private readonly AuthController controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthControllerTests"/> class.
        /// Constructor initializes mock dependencies and controller instance for testing.
        /// </summary>
        public AuthControllerTests()
        {
            this.mockTokenGenerator = new Mock<IJwtTokenGenerator>();
            this.controller = new AuthController(this.mockTokenGenerator.Object);
        }

        /// <summary>
        /// Test to ensure that when valid credentials are provided,
        /// the controller returns a 200 OK result with a valid JWT token.
        /// </summary>
        [Fact]
        public void Login_ValidCredentials_ReturnsOkWithToken()
        {
            // Arrange
            var mockTokenGenerator = new Mock<IJwtTokenGenerator>();
            var dummyToken = "dummy-jwt-token";
            mockTokenGenerator.Setup(tg => tg.GenerateToken("admin")).Returns(dummyToken);

            var controller = new AuthController(mockTokenGenerator.Object);
            var request = new LoginRequest
            {
                Username = "admin",
                Password = "password",
            };

            // Act
            var result = controller.Login(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var tokenProp = okResult.Value.GetType().GetProperty("token");
            var tokenValue = tokenProp.GetValue(okResult.Value, null) as string;
            Assert.Equal(dummyToken, tokenValue);
        }

        /// <summary>
        /// Test to verify that when invalid credentials are provided,
        /// the controller returns a 401 Unauthorized result.
        /// </summary>
        [Fact]
        public void Login_InvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var mockTokenGenerator = new Mock<IJwtTokenGenerator>();
            var controller = new AuthController(mockTokenGenerator.Object);
            var request = new LoginRequest
            {
                Username = "user",
                Password = "wrongpassword",
            };

            // Act
            var result = controller.Login(request);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }

        /// <summary>
        /// Test to confirm that when a null login request is passed to the controller,
        /// it returns a 400 Bad Request response.
        /// </summary>
        [Fact]
        public void Login_NullRequest_ReturnsBadRequest()
        {
            // Arrange
            var mockTokenGenerator = new Mock<IJwtTokenGenerator>();
            var controller = new AuthController(mockTokenGenerator.Object);

            // Act
            var result = controller.Login(null);

            // Assert
            Assert.IsType<BadRequestResult>(result); // Requires null-check logic in the controller
        }
    }
}
