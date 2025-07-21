using AutoMapper;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;

/// <summary>
/// AutoMapper profile class for configuring mappings between Book model and its DTOs.
/// </summary>
public class BookProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="BookProfile"/> class.
    /// Constructor where mapping configurations are defined.
    /// </summary>
    public BookProfile()
    {
        // Maps from Book model to BookReadDto
        // Used when sending data from the API to the client (read operation)
        this.CreateMap<Book, BookReadDto>();

        // Maps from BookCreateDto to Book model
        // Used when receiving data from the client to create a new Book
        this.CreateMap<BookCreateDto, Book>();
    }
}