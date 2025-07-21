using BookManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;

/// <summary>
/// Unit tests for BooksController using mocked IBookService to validate controller behavior independently from the service layer.
/// </summary>
public class BooksControllerTests
{
    private readonly Mock<IBookService> mockService;
    private readonly BooksController controller;

    public BooksControllerTests()
    {
        this.mockService = new Mock<IBookService>();
        this.controller = new BooksController(this.mockService.Object);
    }

    /// <summary>
    /// Test to ensure GetAll() returns an HTTP 200 OK response with a list of books.
    /// Verifies that the controller can successfully retrieve and return all book records.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfBooks()
    {
        var books = new List<BookReadDto> { new BookReadDto { Id = 1, Title = "Test Book" } };
        this.mockService.Setup(s => s.GetAllBooks()).ReturnsAsync(books);

        var result = await this.controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnBooks = Assert.IsAssignableFrom<IEnumerable<BookReadDto>>(okResult.Value);
        Assert.Single(returnBooks);
    }

    /// <summary>
    /// Test to verify Get(id) returns an HTTP 200 OK response when a valid book ID is provided.
    /// Confirms that the correct book data is returned for the given ID.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Get_WithValidId_ReturnsOkResult()
    {
        var book = new BookReadDto { Id = 1, Title = "Test Book" };
        this.mockService.Setup(s => s.GetBookById(1)).ReturnsAsync(book);

        var result = await this.controller.Get(1);

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnBook = Assert.IsType<BookReadDto>(okResult.Value);
        Assert.Equal(1, returnBook.Id);
    }

    /// <summary>
    /// Test to verify Get(id) returns an HTTP 404 Not Found response when the book does not exist.
    /// Confirms that invalid or non-existent IDs are handled properly.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
    [Fact]
    public async Task Get_WithInvalidId_ReturnsNotFound()
    {
        this.mockService.Setup(s => s.GetBookById(99)).ReturnsAsync((BookReadDto)null);

        var result = await this.controller.Get(99);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    /// <summary>
    /// Test to ensure Create() returns an HTTP 201 Created response after successfully creating a book.
    /// Verifies that the controller properly saves and returns the newly created book data.
    /// </summary>
    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        var newBook = new BookCreateDto { Title = "New Book" };
        var createdBook = new BookReadDto { Id = 1, Title = "New Book" };

        this.mockService.Setup(s => s.CreateBook(newBook)).ReturnsAsync(createdBook);

        var result = await this.controller.Create(newBook);

        var createdAtAction = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnBook = Assert.IsType<BookReadDto>(createdAtAction.Value);
        Assert.Equal("New Book", returnBook.Title);
    }

    /// <summary>
    /// Test to confirm Update() returns an HTTP 204 No Content response when a book is successfully updated.
    /// Validates that updates are properly processed by the controller for existing book IDs.
    /// </summary>
    [Fact]
    public async Task Update_WithValidId_ReturnsNoContent()
    {
        this.mockService.Setup(s => s.UpdateBook(1, It.IsAny<BookCreateDto>())).ReturnsAsync(true);

        var result = await this.controller.Update(1, new BookCreateDto());

        Assert.IsType<NoContentResult>(result);
    }

    /// <summary>
    /// Test to verify Update() returns an HTTP 404 Not Found response when the book ID does not exist.
    /// Ensures the controller handles invalid updates gracefully.
    /// </summary>
    [Fact]
    public async Task Update_WithInvalidId_ReturnsNotFound()
    {
        this.mockService.Setup(s => s.UpdateBook(99, It.IsAny<BookCreateDto>())).ReturnsAsync(false);

        var result = await this.controller.Update(99, new BookCreateDto());

        Assert.IsType<NotFoundResult>(result);
    }

    /// <summary>
    /// Test to verify Delete() returns an HTTP 204 No Content response when a book is successfully deleted.
    /// Ensures that valid book deletions are processed correctly by the controller.
    /// </summary>
    [Fact]
    public async Task Delete_WithValidId_ReturnsNoContent()
    {
        this.mockService.Setup(s => s.DeleteBook(1)).ReturnsAsync(true);

        var result = await this.controller.Delete(1);

        Assert.IsType<NoContentResult>(result);
    }

    /// <summary>
    /// Test to confirm Delete() returns an HTTP 404 Not Found response when the book ID does not exist.
    /// Verifies that the controller handles delete requests for non-existent books correctly.
    /// </summary>
    [Fact]
    public async Task Delete_WithInvalidId_ReturnsNotFound()
    {
        this.mockService.Setup(s => s.DeleteBook(99)).ReturnsAsync(false);

        var result = await this.controller.Delete(99);

        Assert.IsType<NotFoundResult>(result);
    }
}
