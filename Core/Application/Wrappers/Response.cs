using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class Response<T>
    {
        public Response()
        {

        }

        public Response(bool succeeded)
        {
            Succeeded = succeeded;
        }

        public Response(T data, string? message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(string message)
        {
            this.Message = message;
            this.Succeeded = true;
        }

        public Response(string message, bool succeeded = false)
        {
            Succeeded = succeeded;
            Message = message;
        }

        public Response(List<string> errors)
        {
            this.Errors = errors;
            this.Succeeded = false;
        }

     

        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }
    }
}
