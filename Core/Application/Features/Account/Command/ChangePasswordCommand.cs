using Application.Wrappers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Account.Command
{
    public class ChangePasswordCommand : IRequest<Response<bool>>
    {
        public string? UserID { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Response<bool>>
        {
            private readonly UserManager<User> _userManager;

            public ChangePasswordCommandHandler(UserManager<User> userManager)
            {
                _userManager = userManager;
            }

            public async Task<Response<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserID);

                if (user == null)
                {
                    throw new KeyNotFoundException("Current user not found");
                }

                var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

                if (!result.Succeeded)
                {
                    return new Response<bool>(message: result.Errors.Select(x => x.Description).First(), false);
                }

                return new Response<bool>(true);
            }
        }
    }
}
