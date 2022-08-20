using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notes.Queries.GetNoteQuery
{
    public class GetNoteQuery : IRequest<Response<NoteDTO>>
    {
        public string UserID { get; set; }
        public string NoteID { get; set; }

        public class GetNoteQueryHandler : IRequestHandler<GetNoteQuery, Response<NoteDTO>>
        {

            private readonly UserManager<User> _userManager;
            private readonly IRepositoryAsync<Note> _repositoryAsync;
            private readonly IMapper _mapper;

            public GetNoteQueryHandler(UserManager<User> userManager, IRepositoryAsync<Note> repositoryAsync, IMapper mapper)
            {
                _userManager = userManager;
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<NoteDTO>> Handle(GetNoteQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var note = await _repositoryAsync.GetByIdAsync(request.NoteID);
                if (note == null || note.User?.Id != user.Id)
                {
                    throw new KeyNotFoundException($"Note {request.NoteID} not found");
                }
               
                return new Response<NoteDTO>(_mapper.Map<NoteDTO>(note));
            }
        }
    }
}
