namespace BookManagementAPI.Validators
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CurrentOrPastYearAttribute : ValidationAttribute
    {
        // If the base method accepts a nullable value:
        public override bool IsValid(object? value)

        {
            if (value is int year)
            {
                return year <= DateTime.UtcNow.Year;
            }

            return false;
        }
    }
}