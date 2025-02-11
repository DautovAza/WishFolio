namespace WishFolio.Application.UseCases.UserProfile.Queries.GetProfile.Dtos;

public record GetProfileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
