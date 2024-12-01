using Application.DTOs;
using Application.Features.Auth.Commands.SignUpCommand;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class CreateMappers : Profile
    {
        public CreateMappers()
        {
            CreateMap<SignUpCommand, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
