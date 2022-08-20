using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;
using FluentValidation;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
           services.AddAutoMapper(Assembly.GetExecutingAssembly());
           services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
           services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}