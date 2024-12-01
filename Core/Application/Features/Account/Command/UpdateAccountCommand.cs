using Application.DTOs;
using Application.Wrappers;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Account.Command
{
    public class UpdateAccountCommand : IRequest<Response<UserDTO>>
    {

        public string? UserID { get; set; }
        public string FullName { get; set; }


        public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, Response<UserDTO>>
        {

            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;

            public UpdateAccountCommandHandler(UserManager<User> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<Response<UserDTO>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                user.FullName = request.FullName;
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return new Response<UserDTO>(message: result.Errors.Select(x => x.Description).First(), false);
                }

                var userDTO = _mapper.Map<UserDTO>(user);
                return new Response<UserDTO>(userDTO);
            }
        }
    }
}
