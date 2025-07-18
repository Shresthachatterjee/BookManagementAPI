using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

/// <summary>
/// Unit tests for the BookService class to verify service logic independently using mock data and dependencies.
/// </summary>
public class BookServiceTests
{
    private readonly Mock<IBookRepository> _mockRepo;
    private readonly IMapper _mapper;
    private readonly BookService _service;
    private readonly Mock<ILogger<BookService>> _mockLogger = new Mock<ILogger<BookService>>();

    public BookServiceTests()
    {
        _mockRepo = new Mock<IBookRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Book, BookReadDto>();
            cfg.CreateMap<BookCreateDto, Book>();
            cfg.CreateMap<BookCreateDto, Book>().ReverseMap();
        });
        _mapper = config.CreateMapper();

        _service = new BookService(_mockRepo.Object, _mapper, _mockLogger.Object);
    }

    /// <summary>
    /// Verifies that the service fetches the entire book list from the repository and converts them for the response.
    /// Test to check if the service can fetch all books and return them properly.
    /// </summary>
    [Fact]
    public async Task GetAllBooks_ReturnsMappedBooks()
    {
       
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Test Book", Author = "Author A" },
            new Book { Id = 2, Title = "Another Book", Author = "Author B" }
        };
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

        var result = await _service.GetAllBooks();

        Assert.Equal(2, result.Count());
        Assert.Contains(result, b => b.Title == "Test Book");
    }

    /// <summary>
    /// Verifies that GetBookById() correctly fetches a specific book and maps it to a DTO when the book exists.
    /// Test to check if the service can fetch a book by its ID and return it when it exists.
    /// <param name="id">
    /// The unique identifier of the book in the database.
    /// This ID is used to locate the specific book record in the databasse system.
    /// </param>
    /// </summary>
    [Fact]
    public async Task GetBookById_BookExists_ReturnsMappedBook()
    {
        var book = new Book { Id = 1, Title = "Test Book", Author = "Author A" };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);

        var result = await _service.GetBookById(1);

        Assert.NotNull(result);
        Assert.Equal("Test Book", result.Title);
    }

    /// <summary>
    /// Checks that GetBookById() returns null if the requested book is not found in the repository.
    /// Test to check if service returns nothing when the book with given ID does not exist.
    /// <param name="id">
    /// The unique identifier of the book in the database.
    /// This ID is used to locate the specific book record in the databasse system.
    /// </param>
    /// </summary>
    [Fact]
    public async Task GetBookById_BookDoesNotExist_ReturnsNull()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Book)null);

        var result = await _service.GetBookById(999);

        Assert.Null(result);
    }

    /// <summary>
    /// Confirms that CreateBook() successfully maps and creates a new book, returning the created book DTO.
    /// Test to ensure that a new book can be added successfully.
    /// </summary>
    [Fact]
    public async Task CreateBook_ValidDto_ReturnsCreatedBook()
    {
        var dto = new BookCreateDto { Title = "New Book", Author = "Author A" };
        var created = new Book { Id = 1, Title = "New Book", Author = "Author A" };

        _mockRepo.Setup(r => r.CreateAsync(It.IsAny<Book>())).ReturnsAsync(created);

        var result = await _service.CreateBook(dto);

        Assert.NotNull(result);
        Assert.Equal("New Book", result.Title);
    }

    /// <summary>
    /// Verifies that UpdateBook() updates an existing book and returns true when the book is found.
    /// Also ensures that the book’s title is changed as per the input.
    /// Test to verify that updating an existing book works and updates the title.
    /// </summary>
    [Fact]
    public async Task UpdateBook_BookExists_ReturnsTrue()
    {
        var existingBook = new Book { Id = 1, Title = "Old Title", Author = "Author" };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingBook);
        _mockRepo.Setup(r => r.UpdateAsync(existingBook)).Returns(Task.CompletedTask);

        var dto = new BookCreateDto { Title = "Updated Title", Author = "Author" };

        var result = await _service.UpdateBook(1, dto);

        Assert.True(result);
        Assert.Equal("Updated Title", existingBook.Title);
    }

    /// <summary>
    /// Ensures UpdateBook() returns false when the book with the specified ID does not exist in the database.
    /// </summary>
    [Fact]
    public async Task UpdateBook_BookDoesNotExist_ReturnsFalse()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Book)null);

        var dto = new BookCreateDto { Title = "Updated Title", Author = "Author" };

        var result = await _service.UpdateBook(1, dto);

        Assert.False(result);
    }

    /// <summary>
    /// Verifies that DeleteBook() deletes the book and returns true when the book exists.
    /// Confirms that the deletion operation is successful.
    /// </summary>
    [Fact]
    public async Task DeleteBook_BookExists_ReturnsTrue()
    {
        var book = new Book { Id = 1, Title = "Delete Me", Author = "Author" };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
        _mockRepo.Setup(r => r.DeleteAsync(book)).Returns(Task.CompletedTask);

        var result = await _service.DeleteBook(1);

        Assert.True(result);
    }

    /// <summary>
    /// Ensures that DeleteBook() returns false when the book to be deleted is not found in the system.
    /// </summary>
    [Fact]
    public async Task DeleteBook_BookDoesNotExist_ReturnsFalse()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Book)null);

        var result = await _service.DeleteBook(1);

        Assert.False(result);
    }
}
