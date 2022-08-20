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

namespace Application.Features.Notes.Commands.UpdateNoteCommand
{
    public class UpdateNoteCommand : IRequest<Response<string>>
    {
        public string? UserID { get; set; }
        public string? NoteID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Response<string>>
        {

            private readonly UserManager<User> _userManager;
            private readonly IRepositoryAsync<Note> _repositoryAsync;

            public UpdateNoteCommandHandler(UserManager<User> userManager, IRepositoryAsync<Note> repositoryAsync)
            {
                _userManager = userManager;
                _repositoryAsync = repositoryAsync;
            }

            public async Task<Response<string>> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
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

                note.Title = request.Title;
                note.Description = request.Description;

                await _repositoryAsync.UpdateAsync(note);
                return new Response<string>(note.ID);
            }
        }
    }
}
