using BookManagementAPI.Models;

/// <summary>
/// Interface for defining CRUD operations related to the Book entity.
/// This interface is implemented by the BookRepository class.
/// </summary>
public interface IBookRepository
{
    /// <summary>
    /// Retrieves a list of all books asynchronously.
    /// </summary>
    /// <returns>A collection of Book objects.</returns>
    Task<IEnumerable<Book>> GetAllAsync();

    /// <summary>
    /// Retrieves a single book based on the given ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the book to retrieve.</param>
    /// <returns>The Book object if found; otherwise, null.</returns>
    Task<Book> GetByIdAsync(int id);

    /// <summary>
    /// Adds a new book to the database asynchronously.
    /// </summary>
    /// <param name="book">The Book object to add.</param>
    /// <returns>The created Book object with generated unique ID.</returns>
    Task<Book> CreateAsync(Book book);

    /// <summary>
    /// Updates an existing book in the database asynchronously.
    /// </summary>
    /// <param name="book">The Book object with updated information.</param>
    Task UpdateAsync(Book book);

    /// <summary>
    /// Deletes an existing book from the database asynchronously.
    /// </summary>
    /// <param name="book">The Book object to delete.</param>
    Task DeleteAsync(Book book);
}

