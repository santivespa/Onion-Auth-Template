using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Features.Auth.Commands.SignUpCommand;
using Application.Features.Notes.Commands.CreateNoteCommand;
using AutoMapper;
using Domain;

namespace Application.Mappings
{
    public class CreateMappers : Profile
    {
        public CreateMappers()
        {
            CreateMap<SignUpCommand, User>();

            CreateMap<User, UserDTO>();

            CreateMap<CreateNoteCommand, Note>();

            CreateMap<Note, NoteDTO>();
        }
    }
}
