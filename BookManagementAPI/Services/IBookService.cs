// <copyright file="IBookService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
#pragma warning disable

using BookManagementAPI.DTOs;

public interface IBookService
{
    Task<IEnumerable<BookReadDto>> GetAllBooks();

    Task<BookReadDto> GetBookById(int id);

    Task<BookReadDto> CreateBook(BookCreateDto dto);

    Task<bool> UpdateBook(int id, BookCreateDto dto);

    Task<bool> DeleteBook(int id);
}
