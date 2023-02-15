using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestPractices.API.Controllers.Common
{
    [ApiController]
    public abstract class BaseAPIController : ControllerBase
    {
        private IMediator _mediator;
        private ILogger<BaseAPIController> _logger;
        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        protected ILogger<BaseAPIController> Logger => _logger ?? (_logger = HttpContext.RequestServices.GetService<ILogger<BaseAPIController>>());

    }
}
