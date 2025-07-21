using BookManagementAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService service;

    /// <summary>
    /// Initializes a new instance of the <see cref="BooksController"/> class.
    /// </summary>
    /// <param name="service"></param>
    public BooksController(IBookService service)
    {
        this.service = service;
    }

    /// <summary>Retrieves a list of all books available in the database.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookReadDto>>> GetAll() =>
        this.Ok(await this.service.GetAllBooks());

    /// <param name="id">
    /// The unique identifier of the book.
    /// This ID is used to locate the specific book record in the  database ssystem.
    /// </param>
    /// <summary>Retrieves the details of a specific book using its unique identifier (ID) created in the database.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<BookReadDto>> Get(int id)
    {
        var book = await this.service.GetBookById(id);
        if (book == null)
        {
            return this.NotFound();
        }

        return this.Ok(book);
    }

    /// <summary>Creates a new book record entry in the database using the provided book details.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpPost]
    public async Task<ActionResult<BookReadDto>> Create(BookCreateDto dto)
    {
        var book = await this.service.CreateBook(dto);
        return this.CreatedAtAction(nameof(this.Get), new { id = book.Id }, book);
    }

    /// <param name="id">
    /// The unique identifier of the books.
    /// This ID is used to locate the specific book record in the database system.
    /// </param>
    /// <summary>Updates the details of an existing book with the specified ID using the provided data.
    /// If the book with the given ID is not found, a 404 Not Found response is returned.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookCreateDto dto)
    {
        if (dto is null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        var success = await this.service.UpdateBook(id, dto);
        if (!success)
        {
            return this.NotFound();
        }

        return this.NoContent();
    }

    /// <param name="id">
    /// The unique identifier of the book to be deleted.
    /// This ID is used to locate the specific book record in the databasse system.
    /// </param>
    /// <summary>Deletes an existing book by its unique identifier (ID). If the book with the specified ID is found, it will be removed from the system.</summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await this.service.DeleteBook(id);
        if (!success)
        {
            return this.NotFound();
        }

        return this.NoContent();
    }
}
