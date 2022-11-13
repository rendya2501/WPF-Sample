using System.Globalization;
using System.Windows.Controls;

namespace ValidationError.ValidationRules
{
    public class NumericValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!string.IsNullOrEmpty((string)value))
            {
                if (!int.TryParse(value.ToString(), out _))
                {
                    return new ValidationResult(false, "整数を入力してください。");
                }
            }
            return new ValidationResult(true, null);
        }

    }
}
