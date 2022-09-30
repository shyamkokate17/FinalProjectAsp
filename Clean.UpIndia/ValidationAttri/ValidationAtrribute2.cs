using System.ComponentModel.DataAnnotations;
using System;

namespace Clean.UpIndia.ValidationAttribute2
{
    
    public class  ValidationOneMonthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;
            //  i.e. exactly one month before is okay....
            if (DateTime.Now.AddMonths(-1).CompareTo(value) <= 0 && DateTime.Now.CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date must be valid before month!!!");
            }
        }
    }
}
