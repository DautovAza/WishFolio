using MediatR;
using Microsoft.AspNetCore.Mvc;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.WebApi.Controllers.Abstractions;

public abstract class ResultHandlerControllerBase : ControllerBase
{
    protected ISender Sender { get; }

    public ResultHandlerControllerBase(ISender sender)
        : base()
    {
        Sender = sender;
    }

    protected async Task<ActionResult<TResponce>> HandleRequestResult<TResponce>(IRequest<Result<TResponce>> request)
        where TResponce : class
    {
        try
        {
            var result = await Sender.Send(request);
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

    protected async Task<ActionResult> HandleRequestResult(IRequest<Result> request)
    {
        try
        {
            var result = await Sender.Send(request);
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
