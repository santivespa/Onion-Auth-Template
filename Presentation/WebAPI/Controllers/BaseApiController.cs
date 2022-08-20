﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : Controller
    {

        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        

        public string UserID
        {
            get
            {
                var claim = User.Claims.FirstOrDefault(x => x.Type == "UserID");
                if(claim == null)
                {
                    throw new UnauthorizedAccessException();
                }
                return claim.Value;
            }
        }
    }
}
