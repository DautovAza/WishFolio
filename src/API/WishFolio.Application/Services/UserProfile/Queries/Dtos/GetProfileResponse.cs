namespace WishFolio.Application.Services.UserProfile.Queries.Dtos;

public class GetProfileResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}
