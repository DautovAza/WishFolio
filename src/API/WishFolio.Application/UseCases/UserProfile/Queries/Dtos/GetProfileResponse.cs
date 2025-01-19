namespace WishFolio.Application.UseCases.UserProfile.Queries.Dtos;

public record GetProfileResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}
