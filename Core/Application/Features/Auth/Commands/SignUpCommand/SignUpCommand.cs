using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.SignUpCommand
{
    public class SignUpCommand : IRequest<Response<UserDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public class SignInCommandHandler : IRequestHandler<SignUpCommand, Response<UserDTO>>
        {

            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly RoleManager<IdentityRole> _roleManager;

            public SignInCommandHandler(UserManager<User> userManager, IMapper mapper, ITokenHelper tokenHelper, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _roleManager = roleManager;
            }

            public async Task<Response<UserDTO>> Handle(SignUpCommand request, CancellationToken cancellationToken)
            {
                var user = _mapper.Map<User>(request);
                user.UserName = user.Email;
              
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await CheckRoles();
                    await _userManager.AddToRoleAsync(user, AppRoles.Admin);

                    var userRoles = new List<string> { AppRoles.Admin };
                    var userDTO = _mapper.Map<UserDTO>(user);

                    userDTO.Token = _tokenHelper.GenerateToken(user, userRoles);
                    return new Response<UserDTO>(userDTO);
                }
                else
                {
                    return new Response<UserDTO>(result.Errors.Select(x => x.Description).First(), false);
                }
            }

            private async Task CheckRoles()
            {
                if (!await _roleManager.RoleExistsAsync(AppRoles.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole() { Name = AppRoles.Admin });
                }
                if (!await _roleManager.RoleExistsAsync(AppRoles.Sales))
                {
                    await _roleManager.CreateAsync(new IdentityRole() { Name = AppRoles.Sales });
                }
            }
        }
    }
}
