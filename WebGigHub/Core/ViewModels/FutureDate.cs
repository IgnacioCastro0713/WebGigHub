using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebGigHub.Core.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        // custom validation
        public override bool IsValid(object value)
        {
            DateTime dateTime;
            var isValid = DateTime.TryParseExact(Convert.ToString(value), 
                "d MMM yyyy", 
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, 
                out dateTime);

            return (isValid && dateTime > DateTime.Now.Subtract(DateTime.Now.TimeOfDay));
        }
    }
}