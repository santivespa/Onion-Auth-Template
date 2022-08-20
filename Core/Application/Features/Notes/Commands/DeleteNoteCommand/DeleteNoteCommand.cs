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

namespace Application.Features.Notes.Commands.DeleteNoteCommand
{
    public class DeleteNoteCommand : IRequest<Response<string>>
    {
        public string? UserID { get; set; }

        public string NoteID { get; set; }

        public class DeleteNoteCommandHandler : IRequestHandler<DeleteNoteCommand, Response<string>>
        {

            private readonly UserManager<User> _userManager;
            private readonly IRepositoryAsync<Note> _repositoryAsync;
            private readonly IMapper _mapper;

            public DeleteNoteCommandHandler(UserManager<User> userManager, IRepositoryAsync<Note> repositoryAsync, IMapper mapper)
            {
                _userManager = userManager;
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<string>> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
            {

                var user = await _userManager.FindByIdAsync(request.UserID);
                if (user == null)
                {
                    throw new UnauthorizedAccessException();
                }

                var note = await _repositoryAsync.GetByIdAsync(request.NoteID);
                if (note == null || note.User?.Id != user.Id)
                {
                    throw new KeyNotFoundException($"Note { request.NoteID } not found");
                }

                await _repositoryAsync.DeleteAsync(note);
                return new Response<string>(note.ID);
             
            }
        }
    }
}
