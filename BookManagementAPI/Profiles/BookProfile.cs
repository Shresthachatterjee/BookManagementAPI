using AutoMapper;
using BookManagementAPI.Models;
using BookManagementAPI.DTOs; 

/// <summary>
/// AutoMapper profile class for configuring mappings between Book model and its DTOs.
/// </summary>
public class BookProfile : Profile
{
    /// <summary>
    /// Constructor where mapping configurations are defined.
    /// </summary>
    public BookProfile()
    {
        // Maps from Book model to BookReadDto
        // Used when sending data from the API to the client (read operation)
        CreateMap<Book, BookReadDto>();

        // Maps from BookCreateDto to Book model
        // Used when receiving data from the client to create a new Book
        CreateMap<BookCreateDto, Book>();
    }
}

