// <copyright file="BookManagementDbContext.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable

using BookManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

public class BookManagementDbContext : DbContext
{
    public BookManagementDbContext(DbContextOptions<BookManagementDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
}