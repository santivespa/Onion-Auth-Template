using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class MainContextRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        public readonly MainContext _context;

        public MainContextRepositoryAsync(MainContext context): base(context)
        {
            _context = context;
        }
    }
}
