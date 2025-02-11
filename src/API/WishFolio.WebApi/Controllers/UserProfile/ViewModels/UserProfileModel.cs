namespace WishFolio.WebApi.Controllers.UserProfile.Models;

public record UserProfileModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
