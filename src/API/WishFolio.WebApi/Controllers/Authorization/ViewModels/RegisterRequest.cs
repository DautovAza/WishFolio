namespace WishFolio.WebApi.Controllers.Authorization.ViewModels;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Password { get; set; }
}
