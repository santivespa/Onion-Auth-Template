using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repository
{
    public class ContextRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        public readonly MainContext _context;

        public ContextRepositoryAsync(MainContext context): base(context)
        {
            _context = context;
        }
    }
}
