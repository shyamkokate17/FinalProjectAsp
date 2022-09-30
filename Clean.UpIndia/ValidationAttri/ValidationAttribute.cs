using System.ComponentModel.DataAnnotations;
using System;

namespace Clean.UpIndia.ValidationAttri
{
    public class ValidationOneYearAttribute : ValidationAttribute

    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;
            // This assumes inclusivity, i.e. exactly one years after is okay
            if (DateTime.Now.AddYears(+1).CompareTo(value) >= 0 && DateTime.Now.CompareTo(value) <= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date must be in the future years!");
            }
        }
    }
}
