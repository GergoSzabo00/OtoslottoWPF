using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace otoslotto
{
    public class NameValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value as string))
            {
                return new ValidationResult(false, "A név nem lehet üres!");
            }

            if (Regex.IsMatch(value as string, "[0-9;_.]"))
            {
                return new ValidationResult(false, "A név csak betűket tartalmazhat!");
            }

            return ValidationResult.ValidResult;
        }
    }
}
