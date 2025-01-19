namespace WishFolio.Domain.Errors;

public record Error(string ErrorCode, string Message)
{
    public bool IsNone() =>
        this == None;

    public static Error None => new(string.Empty, string.Empty);
}