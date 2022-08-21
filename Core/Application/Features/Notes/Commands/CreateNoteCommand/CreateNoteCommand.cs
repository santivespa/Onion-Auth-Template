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

namespace Application.Features.Notes.Commands.CreateNoteCommand
{
    public class CreateNoteCommand : IRequest<Response<string>>
    {
        public string? UserID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Response<string>>
        {

            private readonly UserManager<User> _userManager;
            private readonly IRepositoryAsync<Note> _repositoryAsync;
            private readonly IMapper _mapper;

            public CreateNoteCommandHandler(UserManager<User> userManager, IRepositoryAsync<Note> repositoryAsync, IMapper mapper)
            {
                _userManager = userManager;
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByIdAsync(request.UserID);

                if(user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var note = _mapper.Map<Note>(request);

                note.User = user;

                await _repositoryAsync.AddAsync(note);

                return new Response<string>(note.ID);
            }
        }
    }
}
