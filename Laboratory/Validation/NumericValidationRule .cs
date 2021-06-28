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
    class NumericValidationRule : ValidationRule
    {
        public Type ValidationType { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            
            Regex regex = new Regex(@"^[0-9]*$");
            Match match = regex.Match(value.ToString());
            if (match == null || match == Match.Empty)
            {
                return new ValidationResult(false, "Not number");
            }
            else
            {
                return ValidationResult.ValidResult;
            }
            

        }
    }
}
