using System;
using System.ComponentModel.DataAnnotations;
using BookManagementAPI.Validators;

namespace BookManagementAPI.DTOs
{
    public class BookCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be between 2 and 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Author must be between 2 and 100 characters.")]
        public string Author { get; set; }

        [Range(1450, 2100, ErrorMessage = "Year must be between 1450 and 2100.")]
        [CurrentOrPastYear(ErrorMessage = "Year must not be in the future.")]
        public int Year { get; set; }
    }
}

