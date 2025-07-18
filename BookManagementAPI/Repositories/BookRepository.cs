using BookManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
public class BookRepository : IBookRepository
{
    private readonly BookManagementDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookRepository"/> class with the specified database context.
    /// </summary>
    /// <param name="context">The database context used for accessing book data.</param>
    public BookRepository(BookManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all books from the database asynchronously.
    /// </summary>
    /// <returns>A list of all book entities.</returns>
    public async Task<IEnumerable<Book>> GetAllAsync() =>
        await _context.Books.ToListAsync();

    /// <summary>
    /// Retrieves a single book by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique ID of the book to retrieve.</param>
    /// <returns>The matching book entity if found; otherwise, null.</returns>
    public async Task<Book> GetByIdAsync(int id) =>
        await _context.Books.FindAsync(id);

    /// <summary>
    /// Adds a new book to the database and saves the changes asynchronously.
    /// </summary>
    /// <param name="book">The book entity to be added.</param>
    /// <returns>The newly created book entity.</returns>
    public async Task<Book> CreateAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    /// <summary>
    /// Updates an existing book in the database and saves the changes asynchronously.
    /// </summary>
    /// <param name="book">The book entity with updated values.</param>
    public async Task UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a book from the database and saves the changes asynchronously.
    /// </summary>
    /// <param name="book">The book entity to be deleted.</param>
    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }
}


