using BookManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

public class BookManagementDbContext : DbContext
{
    public BookManagementDbContext(DbContextOptions<BookManagementDbContext> options)
        : base(options) { }

    public DbSet<Book> Books { get; set; }
}