using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Application.Specifications;

namespace Application.Features.Notes.Queries.GetNotesQuery
{
    public class GetNotesQuery : IRequest<Response<List<NoteDTO>>>
    {
        public string UserID { get; set; }

        public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, Response<List<NoteDTO>>>
        {

            private readonly UserManager<User> _userManager;
            private readonly IRepositoryAsync<Note> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetNotesQueryHandler(UserManager<User> userManager, IRepositoryAsync<Note> repositoryAsync, IMapper mapper)
            {
                _userManager = userManager;
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<List<NoteDTO>>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var notes = await _repositoryAsync.ListAsync(new GetNotesByUserSpecification(user.Id));
              
                return new Response<List<NoteDTO>>(_mapper.Map<List<NoteDTO>>(notes));
            }
        }
    }
}
