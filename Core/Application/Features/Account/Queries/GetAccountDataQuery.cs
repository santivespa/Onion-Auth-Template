using Application.DTOs;
using Application.Wrappers;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Account.Queries
{
    public class GetAccountDataQuery : IRequest<Response<UserDTO>>
    {
        public string UserID { get; set; }

        public class GetAccountDataQueryHandler : IRequestHandler<GetAccountDataQuery, Response<UserDTO>>
        {
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;

            public GetAccountDataQueryHandler(UserManager<User> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<Response<UserDTO>> Handle(GetAccountDataQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var userDTO = _mapper.Map<UserDTO>(user);
                return new Response<UserDTO>(userDTO);
            }
        }
    }
}
