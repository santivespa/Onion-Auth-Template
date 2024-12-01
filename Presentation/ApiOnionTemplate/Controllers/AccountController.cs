using ApiWebAdmin.Controllers;
using Application.Features.Account.Command;
using Application.Features.Account.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiOnionTemplate.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController
    {
        [HttpGet("get-account-data")]
        public async Task<IActionResult> GetAccountData()
        {
            return Ok(await Mediator.Send(new GetAccountDataQuery() { UserID = UserID }));
        }

        [HttpPost("update-account")]
        public async Task<IActionResult> UpdateAccount(UpdateAccountCommand command)
        {
            command.UserID = UserID;
            return Ok(await Mediator.Send(command));
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            command.UserID = UserID;
            return Ok(await Mediator.Send(command));
        }
    }
}

