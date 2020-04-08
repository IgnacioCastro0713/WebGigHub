using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebGigHub.Core.ViewModels
{
    public class ValidTime: ValidationAttribute
    {
        // custom validation
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value), 
                "HH:mm", CultureInfo.CurrentCulture,
                DateTimeStyles.None, out dateTime);
            return (isValid);
        }
    }
}