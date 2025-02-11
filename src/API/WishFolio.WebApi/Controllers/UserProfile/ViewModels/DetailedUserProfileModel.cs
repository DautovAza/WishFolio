namespace WishFolio.WebApi.Controllers.UserProfile.Models;

public record DetailedUserProfileModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}
