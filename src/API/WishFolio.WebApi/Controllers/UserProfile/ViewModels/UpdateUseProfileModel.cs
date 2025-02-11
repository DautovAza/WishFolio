namespace WishFolio.WebApi.Controllers.UserProfile.Models;

public record UpdateUseProfileModel 
{
    public string? Name { get; set; }
    public int Age { get; set; }
}
