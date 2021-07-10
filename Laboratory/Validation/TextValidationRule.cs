using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Laboratory.Validation
{
    public class TextValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Regex regex = new Regex(@"^[A-Za-z]*$");

            Match match = regex.Match(value.ToString());
            if (match == null || match == Match.Empty)
            {
                return new ValidationResult(false, "Въведете само букви");
            }

            else
            {
                return ValidationResult.ValidResult;
            }
        }

    }
}
