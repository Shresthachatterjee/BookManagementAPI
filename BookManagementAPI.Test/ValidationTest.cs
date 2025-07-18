using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookManagementAPI.DTOs;
using Xunit;

namespace BookManagementAPI.Tests.DTOs
{
    /// <summary>
    /// Unit tests to validate the rules applied on BookCreateDto properties using data annotations.
    /// </summary>
    public class BookCreateDtoValidationTests
    {
        /// <summary>
        /// Helper method to validate a DTO instance using DataAnnotations.
        /// </summary>
        /// <param name="dto">The BookCreateDto instance to validate.</param>
        /// <returns>A list of validation results (errors) if any.</returns>
        private IList<ValidationResult> ValidateDto(BookCreateDto dto)
        {
            var context = new ValidationContext(dto, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(dto, context, results, validateAllProperties: true);
            return results;
        }

        /// <summary>
        /// Ensures that a valid BookCreateDto passes all validation rules.
        /// </summary>
        [Fact]
        public void BookCreateDto_ValidData_ShouldPassValidation()
        {
            // Arrange
            var dto = new BookCreateDto
            {
                Title = "Effective C#",
                Author = "Bill Wagner",
                Year = DateTime.Now.Year
            };

            // Act
            var results = ValidateDto(dto);

            // Assert
            Assert.Empty(results); // Expecting no validation errors
        }

        /// <summary>
        /// Ensures that missing Title causes a validation failure.
        /// </summary>
        [Fact]
        public void BookCreateDto_MissingTitle_ShouldFailValidation()
        {
            var dto = new BookCreateDto
            {
                Title = null,
                Author = "Author Name",
                Year = 2000
            };

            var results = ValidateDto(dto);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Title is required"));
        }

        /// <summary>
        /// Ensures that a Title that is too short triggers a validation error.
        /// </summary>
        [Fact]
        public void BookCreateDto_ShortTitle_ShouldFailValidation()
        {
            var dto = new BookCreateDto
            {
                Title = "A", // Too short
                Author = "Author Name",
                Year = 2000
            };

            var results = ValidateDto(dto);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Title must be between 2 and 100 characters"));
        }

        /// <summary>
        /// Ensures that missing Author causes a validation failure.
        /// </summary>
        [Fact]
        public void BookCreateDto_MissingAuthor_ShouldFailValidation()
        {
            var dto = new BookCreateDto
            {
                Title = "Some Book",
                Author = null,
                Year = 2000
            };

            var results = ValidateDto(dto);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Author is required"));
        }

        /// <summary>
        /// Ensures that a Year value too far in the past is rejected by validation.
        /// </summary>
        [Fact]
        public void BookCreateDto_YearTooEarly_ShouldFailValidation()
        {
            var dto = new BookCreateDto
            {
                Title = "Old Book",
                Author = "Old Author",
                Year = 1000 // Too early
            };

            var results = ValidateDto(dto);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Year must be between 1450 and 2100"));
        }

        /// <summary>
        /// Ensures that a future Year value (beyond current year) fails validation.
        /// </summary>
        [Fact]
        public void BookCreateDto_YearInFuture_ShouldFailValidation()
        {
            var dto = new BookCreateDto
            {
                Title = "Future Book",
                Author = "Future Author",
                Year = DateTime.Now.Year + 1 // Future year
            };

            var results = ValidateDto(dto);

            Assert.Contains(results, r => r.ErrorMessage.Contains("Year must not be in the future"));
        }
    }
}
