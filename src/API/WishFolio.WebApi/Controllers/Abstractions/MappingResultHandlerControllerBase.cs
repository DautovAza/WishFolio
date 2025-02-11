using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WishFolio.Domain.Shared.ResultPattern;

namespace WishFolio.WebApi.Controllers.Abstractions;

public abstract class MappingResultHandlerControllerBase : ResultHandlerControllerBase
{
    protected IMapper Mapper { get; }

    protected MappingResultHandlerControllerBase(ISender sender, IMapper mapper)
        : base(sender)
    {
        Mapper = mapper;
    }

    protected async Task<ActionResult<TMapping>> HandleRequestResult<TResponce, TMapping>(IRequest<Result<TResponce>> request)
        where TResponce : class
    {
        try
        {
            var result = await Sender.Send(request);
            if (result.IsSuccess)
            {
                var model = Mapper.Map<TMapping>(result.Value);
                return Ok(model);
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