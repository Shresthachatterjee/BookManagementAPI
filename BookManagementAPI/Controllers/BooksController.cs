using Microsoft.AspNetCore.Mvc;
using BookManagementAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;

    public BooksController(IBookService service)
    {
        _service = service;
    }

    /// <summary>Retrieves a list of all books available in the database.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookReadDto>>> GetAll() =>
        Ok(await _service.GetAllBooks());

    /// <param name="id">
    /// The unique identifier of the book.
    /// This ID is used to locate the specific book record in the  database ssystem.
    /// </param>
    /// <summary>Retrieves the details of a specific book using its unique identifier (ID) created in the database.</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<BookReadDto>> Get(int id)
    {
        var book = await _service.GetBookById(id);
        if (book == null) return NotFound();
        return Ok(book);
    }

    /// <summary>Creates a new book record entry in the database using the provided book details.</summary>
    [HttpPost]
    public async Task<ActionResult<BookReadDto>> Create(BookCreateDto dto)
    {
        var book = await _service.CreateBook(dto);
        return CreatedAtAction(nameof(Get), new { id = book.Id }, book);
    }

    /// <param name="id">
    /// The unique identifier of the books.
    /// This ID is used to locate the specific book record in the database system.
    /// </param>
    /// <summary>Updates the details of an existing book with the specified ID using the provided data.
    /// If the book with the given ID is not found, a 404 Not Found response is returned.</summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookCreateDto dto)
    {
        var success = await _service.UpdateBook(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    /// <param name="id">
    /// The unique identifier of the book to be deleted.
    /// This ID is used to locate the specific book record in the databasse system.
    /// </param>
    /// <summary>Deletes an existing book by its unique identifier (ID). If the book with the specified ID is found, it will be removed from the system.</summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteBook(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
