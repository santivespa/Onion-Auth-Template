using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Auth.Commands.SignUpCommand
{
    public class SignUpCommand : IRequest<Response<UserDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }


        public class SignInCommandHandler : IRequestHandler<SignUpCommand, Response<UserDTO>>
        {

            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;

            public SignInCommandHandler(UserManager<User> userManager, IMapper mapper, ITokenHelper tokenHelper)
            {
                _userManager = userManager;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
            }

            public async Task<Response<UserDTO>> Handle(SignUpCommand request, CancellationToken cancellationToken)
            {

                var user = _mapper.Map<User>(request);

                var result = await _userManager.CreateAsync(user, request.Password);

                if(result.Succeeded)
                {
                    var userDTO = _mapper.Map<UserDTO>(user);
                    userDTO.Token = _tokenHelper.GenerateToken(user);
                    return new Response<UserDTO>(userDTO);
                }
                else
                {
                    return new Response<UserDTO>(errors: result.Errors.Select(x=>x.Description).ToList());
                }

            }
        }

    }
}
