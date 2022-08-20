using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Application.Wrappers;
using Application.DTOs;
using Domain;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Application.Interfaces;

namespace Application.Features.Auth.Commands.LoginCommand
{
    public class LoginCommand : IRequest<Response<UserDTO>>
    {

        public string EmailOrUserName { get; set; }
        public string Password { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, Response<UserDTO>>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;

            public LoginCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager, IMapper mapper, ITokenHelper tokenHelper)
            {
                _signInManager = signInManager;
                _userManager = userManager;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
            }

            public async Task<Response<UserDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
            {

                var user  = await _userManager.FindByEmailAsync(request.EmailOrUserName);

                if(user == null)
                {
                    user = await _userManager.FindByNameAsync(request.EmailOrUserName);
                }

                if (user == null)
                {
                    return InvalidLoginAttempt();
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    var userDTO = _mapper.Map<UserDTO>(user);
                    userDTO.Token = _tokenHelper.GenerateToken(user);
                    return new Response<UserDTO>(userDTO);
                }
                else
                {
                    return InvalidLoginAttempt();
                }
            }

            private Response<UserDTO> InvalidLoginAttempt()
            {
                return new Response<UserDTO>("Invalid Login Attempt", false);
            }
        }
    }
}
