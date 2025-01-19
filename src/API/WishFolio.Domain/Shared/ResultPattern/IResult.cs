using WishFolio.Domain.Errors;

namespace WishFolio.Domain.Shared.ResultPattern;

public interface IResult
{
    bool IsSuccess { get; }
    bool IsFailure { get; }
    Error[] Errors { get; }
}
