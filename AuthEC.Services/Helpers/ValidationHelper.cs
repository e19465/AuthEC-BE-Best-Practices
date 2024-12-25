using System.ComponentModel.DataAnnotations;

namespace Services.Helpers
{
    /// <summary>
    /// This is re-usable class to validate model binding
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// This is a static method to validate model binding inside ValidateModel class
        /// </summary>
        /// <param name="classToValidate"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static void ValidateModelBinding(object classToValidate)
        {
            if(classToValidate == null)
            {
                throw new ArgumentNullException(nameof(classToValidate));
            }

            ValidationContext validationContext = new ValidationContext(classToValidate);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(classToValidate, validationContext, validationResults, true);

            if (!isValid)
            {
                var errorObject = new Dictionary<string, string>();
                foreach (ValidationResult validationResult in validationResults)
                {
                    if (validationResult.ErrorMessage != null)
                    {
                        errorObject["error"] = validationResult.ErrorMessage;
                    }
                }
                var errorMessage = string.Join("; ", errorObject.Select(e => $"{e.Key}: {e.Value}"));
                throw new ArgumentException(errorMessage);
            }
        }
    }
}
