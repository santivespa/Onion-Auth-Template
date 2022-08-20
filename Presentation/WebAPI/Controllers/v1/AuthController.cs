using Application.Features.Auth.Commands.LoginCommand;
using Application.Features.Auth.Commands.SignUpCommand;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class AuthController : BaseApiController
    {
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignUpCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
