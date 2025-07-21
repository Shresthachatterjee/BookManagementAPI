using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;

/// <summary>
/// Provides business logic for managing books, including operations such as
/// retrieving, creating, updating, and deleting books.
/// </summary>
public class BookService : IBookService
{
    private readonly IBookRepository repository;
    private readonly IMapper _mapper;
    private readonly ILogger<BookService> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="BookService"/> class.
    /// Initializes a new instance of the BookService with injected repository,
    /// AutoMapper, and logging components.
    /// </summary>
    public BookService(IBookRepository repository, IMapper mapper, ILogger<BookService> logger)
    {
        this.repository = repository;
        _mapper = mapper;
        this.logger = logger;
    }

    /// <summary>
    /// Fetches all books available in the system from the database,
    /// and converts them into a simplified format for external use.
    /// </summary>
    /// <returns>A list of all books with basic information for display or API response.</returns>
    public async Task<IEnumerable<BookReadDto>> GetAllBooks()
    {
        logger.LogInformation("Fetching all books.");
        var books = await repository.GetAllAsync();
        return _mapper.Map<IEnumerable<BookReadDto>>(books);
    }

    /// <summary>
    /// Retrieves details of a specific book using its unique ID.
    /// If the book is found, it is converted into a user-friendly format.
    /// </summary>
    /// <param name="id">The unique identifier of the book to retrieve.</param>
    /// <returns>Detailed information of the selected book, or null if not found.</returns>
    public async Task<BookReadDto> GetBookById(int id)
    {
        logger.LogInformation("Fetching book with ID {BookId}.", id);
        var book = await repository.GetByIdAsync(id);
        if (book == null)
        {
            logger.LogWarning("Book with ID {BookId} not found.", id);
            return null;
        }

        return _mapper.Map<BookReadDto>(book);
    }

    /// <summary>
    /// Adds a new book to the system using the provided data.
    /// The new book is saved to the database and returned in a simplified format.
    /// </summary>
    /// <param name="dto">Data provided by the user to create a new book.</param>
    /// <returns>The newly created book with its system-generated details.</returns>
    public async Task<BookReadDto> CreateBook(BookCreateDto dto)
    {
        logger.LogInformation("Creating a new book.");
        var book = _mapper.Map<Book>(dto);
        var created = await repository.CreateAsync(book);
        logger.LogInformation("Book with ID {BookId} created successfully.", created.Id);
        return _mapper.Map<BookReadDto>(created);
    }

    /// <summary>
    /// Updates the details of an existing book using the given ID and updated data.
    /// If the book is not found, the operation fails.
    /// </summary>
    /// <param name="id">The ID of the book to update.</param>
    /// <param name="dto">The new values to apply to the book.</param>
    /// <returns>True if the update is successful, otherwise false.</returns>
    public async Task<bool> UpdateBook(int id, BookCreateDto dto)
    {
        logger.LogInformation("Updating book with ID {BookId}.", id);
        var book = await repository.GetByIdAsync(id);
        if (book == null)
        {
            logger.LogWarning("Cannot update. Book with ID {BookId} not found.", id);
            return false;
        }

        _mapper.Map(dto, book);
        await repository.UpdateAsync(book);
        logger.LogInformation("Book with ID {BookId} updated successfully.", id);
        return true;
    }

    /// <summary>
    /// Removes a book from the system using its ID.
    /// If the book does not exist, the deletion is not performed.
    /// </summary>
    /// <param name="id">The ID of the book to delete.</param>
    /// <returns>True if the book is successfully deleted, otherwise false.</returns>
    public async Task<bool> DeleteBook(int id)
    {
        logger.LogInformation("Deleting book with ID {BookId}.", id);
        var book = await repository.GetByIdAsync(id);
        if (book == null)
        {
            logger.LogWarning("Cannot delete. Book with ID {BookId} not found.", id);
            return false;
        }

        await this.repository.DeleteAsync(book);
        this.logger.LogInformation("Book with ID {BookId} deleted successfully.", id);
        return true;
    }
}
