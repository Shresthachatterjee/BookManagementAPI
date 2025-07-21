// <copyright file="LoginRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookManagementAPI.Controllers
{
    /// <summary>
    /// Model representing the login request payload.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Gets or sets the username of the user attempting to log in.
        /// </summary>
        required public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the user attempting to log in.
        /// </summary>
        required public string Password { get; set; }
    }
}
