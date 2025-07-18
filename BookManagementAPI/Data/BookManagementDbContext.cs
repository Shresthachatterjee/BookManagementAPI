using Microsoft.EntityFrameworkCore;
using BookManagementAPI.Models;

public class BookManagementDbContext : DbContext
{
    public BookManagementDbContext(DbContextOptions<BookManagementDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
}

