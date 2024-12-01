using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.RenewCommand
{
    public class RenewCommand : IRequest<Response<UserDTO>>
    {

        public string? UserID { get; set; }

        public class RenewCommandHandler : IRequestHandler<RenewCommand, Response<UserDTO>>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;

            public RenewCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper, ITokenHelper tokenHelper)
            {
                _signInManager = signInManager;
                _userManager = userManager;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
            }

            public async Task<Response<UserDTO>> Handle(RenewCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserID);
                if (user == null)
                {
                    return InvalidLoginAttempt();
                }

                var userDTO = _mapper.Map<UserDTO>(user);
                var userRoles = await _userManager.GetRolesAsync(user);

                userDTO.Token = _tokenHelper.GenerateToken(user, userRoles);
                return new Response<UserDTO>(userDTO);
            }

            private Response<UserDTO> InvalidLoginAttempt()
            {
                return new Response<UserDTO>("Invalid Login Attempt", false);
            }
        }
    }
}
