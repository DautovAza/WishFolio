namespace WishFolio.Domain.Abstractions.ReadModels.Users;

public class UserProfileReadModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
}
