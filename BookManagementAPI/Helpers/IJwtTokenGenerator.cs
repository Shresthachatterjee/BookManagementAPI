// <copyright file="IJwtTokenGenerator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable

namespace BookManagementAPI.Helpers
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string username);
    }
}
