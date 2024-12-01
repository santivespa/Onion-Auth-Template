using Application.Wrappers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Application.Features.Auth.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }

        public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Response<bool>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IEmailSender _emailSender;
            private readonly IConfiguration _configuration;

            public ResetPasswordCommandHandler(UserManager<User> userManager, IConfiguration configuration, IEmailSender emailSender)
            {
                _userManager = userManager;
                _emailSender = emailSender;
                _configuration = configuration;
            }

            public async Task<Response<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                {
                    throw new UnauthorizedAccessException();

                }
                var codeDecodedBytes = WebEncoders.Base64UrlDecode(request.Token);
                var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);

                var result = await _userManager.ResetPasswordAsync(user, codeDecoded, request.Password);
                if (result.Succeeded)
                {
                    return new Response<bool>(true);
                }
                else
                {
                    return new Response<bool>("Invalid password", false);
                }
            }
        }
    }
}
