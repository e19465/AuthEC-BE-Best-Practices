using System.ComponentModel.DataAnnotations;
using System.Net;
using AuthEC.Services.Helpers;

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
		/// <param name="classToValidate">Model class to be Validated</param>
		public static void ValidateModelBinding(object classToValidate)
        {
            if(classToValidate == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, "Model is null");

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
                throw new CustomException(HttpStatusCode.BadRequest, errorMessage);
			}
        }
    }
}
