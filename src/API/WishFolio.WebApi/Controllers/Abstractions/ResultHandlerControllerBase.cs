using MediatR;
using Microsoft.AspNetCore.Mvc;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.WebApi.Controllers.Abstractions;

public abstract class ResultHandlerControllerBase : ControllerBase
{
    private readonly ISender _sender;

    public ResultHandlerControllerBase(ISender sender)
        : base()
    {
        _sender = sender;
    }

    protected async Task<ActionResult<TResponce>> HandleResultResponseForRequest<TResponce>(IRequest<Result<TResponce>> request)
        where TResponce : class
    {
        try
        {
            var result = await _sender.Send(request);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            var error = result.Errors.First();
            return Problem(error.ErrorCode, error.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    protected async Task<ActionResult> HandleResultResponseForRequest(IRequest<Result> request)
    {
        try
        {
            var result = await _sender.Send(request);
            if (result.IsSuccess)
            {
                return Ok();
            }
            var error = result.Errors.First();
            return Problem(error.ErrorCode, error.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
