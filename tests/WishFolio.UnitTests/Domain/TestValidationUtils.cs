using System.ComponentModel.DataAnnotations;

namespace WishFolio.UnitTests.Domain;

public static class TestValidationUtils
{
    public static IList<ValidationResult> ValidateModel(object model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model, null, null);
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }
}