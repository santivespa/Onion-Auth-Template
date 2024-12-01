using Application.Features.Auth.Commands.LoginCommand;
using Application.Features.Auth.Commands.RecoverPasswordCommand;
using Application.Features.Auth.Commands.RenewCommand;
using Application.Features.Auth.Commands.ResetPassword;
using Application.Features.Auth.Commands.SignUpCommand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebAdmin.Controllers
{
    public class AuthController : BaseApiController
    {
      
        private readonly ILogger<AuthController> _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("recover-password")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("set-password")]
        public async Task<IActionResult> SetPassword(ResetPasswordCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("renew-token")]
        [Authorize]
        public async Task<IActionResult> Renew()
        {
            return Ok(await Mediator.Send(new RenewCommand() { UserID = UserID }));
        }

    }
}
