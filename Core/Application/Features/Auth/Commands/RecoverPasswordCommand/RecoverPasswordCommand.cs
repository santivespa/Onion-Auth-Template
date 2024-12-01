using Application.Wrappers;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Application.Features.Auth.Commands.RecoverPasswordCommand
{
    public class RecoverPasswordCommand : IRequest<Response<bool>>
    {

        public string Email { get; set; }

        public class RecoverPasswordCommandHandler : IRequestHandler<RecoverPasswordCommand, Response<bool>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IEmailSender _emailSender;
            private readonly IConfiguration _configuration;

            public RecoverPasswordCommandHandler(UserManager<User> userManager, IConfiguration configuration, IEmailSender emailSender)
            {
                _userManager = userManager;
                _emailSender = emailSender;
                _configuration = configuration;
            }

            public async Task<Response<bool>> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _userManager.FindByEmailAsync(request.Email);

                    if (user == null)
                    {
                        return new Response<bool>(true);
                    }

                    var tokenGenerated = await _userManager.GeneratePasswordResetTokenAsync(user);
                    byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(tokenGenerated);
                    var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);

                    string to = request.Email;
                    string subject = $"Recover Password";
                    var url = $"{_configuration["AppSettings:Client_URL"]}/auth/reset-password?token={codeEncoded}&email={user.Email}";
                    string body = $"<a target=\"_blank\" href=\"{url}\">Click here<a/> to reset your password.";
                    await _emailSender.SendEmailAsync(to, subject, body);

                    return new Response<bool>(true);
                }
                catch (Exception ex)
                {
                    return null;
                }
               
            }
        }
    }
}
