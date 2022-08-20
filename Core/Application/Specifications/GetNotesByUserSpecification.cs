using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Specifications
{
    internal class GetNotesByUserSpecification : Specification<Note>
    {
        public GetNotesByUserSpecification(string userID)
        {
            Query.Where(x => x.User.Id == userID);
        }
    }
}
