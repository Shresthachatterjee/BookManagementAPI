using System;
using System.ComponentModel.DataAnnotations;

namespace BookManagementAPI.Validators
{
    public class CurrentOrPastYearAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int year)
            {
                return year <= DateTime.UtcNow.Year;
            }
            return false;
        }
    }
}

