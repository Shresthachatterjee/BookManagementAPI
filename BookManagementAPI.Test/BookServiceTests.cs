// <copyright file="BookServiceTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1600
#pragma warning disable
// <copyright file="BookServiceTests.cs" company="PlaceholderCompany">

// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;
using Microsoft.Extensions.Logging;
using Moq;

/// <summary>
/// Unit tests for the BookService class to verify service logic independently using mock data and dependencies.
/// </summary>
public class BookServiceTests
{
    private readonly Mock<IBookRepository> mockRepo;
    private readonly IMapper mapper;
    private readonly BookService service;
    private readonly Mock<ILogger<BookService>> mockLogger = new Mock<ILogger<BookService>>();

    public BookServiceTests()
    {
        this.mockRepo = new Mock<IBookRepository>();

        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Book, BookReadDto>();
            cfg.CreateMap<BookCreateDto, Book>();
            cfg.CreateMap<BookCreateDto, Book>().ReverseMap();
        });
        this.mapper = config.CreateMapper();

        this.service = new BookService(this.mockRepo.Object, this.mapper, this.mockLogger.Object);
    }

    /// <summary>
    /// Verifies that the service fetches the entire book list from the repository and converts them for the response.
    /// Test to check if the service can fetch all books and return them properly.
    /// </summary>
    [Fact]
#pragma warning disable SA1615 // Element return value should be documented
    public async Task GetAllBooks_ReturnsMappedBooks()
#pragma warning restore SA1615 // Element return value should be documented
    {
        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Test Book", Author = "Author A" },
            new Book { Id = 2, Title = "Another Book", Author = "Author B" },
        };
        this.mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(books);

        var result = await this.service.GetAllBooks();

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
#pragma warning disable SA1615 // Element return value should be documented
    public async Task GetBookById_BookExists_ReturnsMappedBook()
#pragma warning restore SA1615 // Element return value should be documented
    {
        var book = new Book { Id = 1, Title = "Test Book", Author = "Author A" };
        this.mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);

        var result = await this.service.GetBookById(1);

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
#pragma warning disable SA1615 // Element return value should be documented
    public async Task GetBookById_BookDoesNotExist_ReturnsNull()
#pragma warning restore SA1615 // Element return value should be documented
    {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        this.mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Book)null);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        var result = await this.service.GetBookById(999);

        Assert.Null(result);
    }

    /// <summary>
    /// Confirms that CreateBook() successfully maps and creates a new book, returning the created book DTO.
    /// Test to ensure that a new book can be added successfully.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task CreateBook_ValidDto_ReturnsCreatedBook()
    {
        var dto = new BookCreateDto { Title = "New Book", Author = "Author A" };
        var created = new Book { Id = 1, Title = "New Book", Author = "Author A" };

        this.mockRepo.Setup(r => r.CreateAsync(It.IsAny<Book>())).ReturnsAsync(created);

        var result = await this.service.CreateBook(dto);

        Assert.NotNull(result);
        Assert.Equal("New Book", result.Title);
    }

    /// <summary>
    /// Verifies that UpdateBook() updates an existing book and returns true when the book is found.
    /// Also ensures that the book’s title is changed as per the input.
    /// Test to verify that updating an existing book works and updates the title.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task UpdateBook_BookExists_ReturnsTrue()
    {
        var existingBook = new Book { Id = 1, Title = "Old Title", Author = "Author" };
        this.mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingBook);
        this.mockRepo.Setup(r => r.UpdateAsync(existingBook)).Returns(Task.CompletedTask);

        var dto = new BookCreateDto { Title = "Updated Title", Author = "Author" };

        var result = await this.service.UpdateBook(1, dto);

        Assert.True(result);
        Assert.Equal("Updated Title", existingBook.Title);
    }

    /// <summary>
    /// Ensures UpdateBook() returns false when the book with the specified ID does not exist in the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task UpdateBook_BookDoesNotExist_ReturnsFalse()
    {
#pragma warning disable CS8620
#pragma warning disable CS8600
        this.mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Book)null);
#pragma warning restore CS8600
#pragma warning restore CS8620

        var dto = new BookCreateDto { Title = "Updated Title", Author = "Author" };

        var result = await this.service.UpdateBook(1, dto);

        Assert.False(result);
    }

    /// <summary>
    /// Verifies that DeleteBook() deletes the book and returns true when the book exists.
    /// Confirms that the deletion operation is successful.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task DeleteBook_BookExists_ReturnsTrue()
    {
        var book = new Book { Id = 1, Title = "Delete Me", Author = "Author" };
        this.mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(book);
        this.mockRepo.Setup(r => r.DeleteAsync(book)).Returns(Task.CompletedTask);

        var result = await this.service.DeleteBook(1);

        Assert.True(result);
    }

    /// <summary>
    /// Ensures that DeleteBook() returns false when the book to be deleted is not found in the system.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task DeleteBook_BookDoesNotExist_ReturnsFalse()
    {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        this.mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Book)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        var result = await this.service.DeleteBook(1);

        Assert.False(result);
    }
}
