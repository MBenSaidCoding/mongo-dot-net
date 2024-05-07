using System.ComponentModel.DataAnnotations;
using WashingtonStoreWebApi.Models;

namespace WashingtonStoreWebApi.Validators
{
    public static class ProductValidator
    {
        public static List<ValidationResult> Validate(this Product product)
        {
            var validationContext = new ValidationContext(product);
            var validationResults = new List<ValidationResult>();

            Validator.TryValidateObject(product, validationContext, validationResults,false);
            return validationResults;
        }
    }
}