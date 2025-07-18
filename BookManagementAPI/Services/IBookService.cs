using BookManagementAPI.DTOs;

public interface IBookService
{
    Task<IEnumerable<BookReadDto>> GetAllBooks();
    Task<BookReadDto> GetBookById(int id);
    Task<BookReadDto> CreateBook(BookCreateDto dto);
    Task<bool> UpdateBook(int id, BookCreateDto dto);
    Task<bool> DeleteBook(int id);
}
