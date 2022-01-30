using CardOrgAPI.Constants;
using CardOrgAPI.Contexts;
using CardOrgAPI.Extensions;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Repositories
{
    public class YearRepository : IYearRepository
    {
        private readonly CardOrgContext _context;

        public YearRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Year>> GetYearsAsync(int page, CancellationToken cancellationToken)
        {
           return await _context.Years
                .Skip(page.PageSkip())
                .Take(RepositoryConstants.PageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Year>> SearchYearsAsync(int year, int page, CancellationToken cancellationToken)
        {
            return await _context.Years
                .Where(x => x.BeginningYear == year || x.EndingYear == year)
                .Skip(page.PageSkip())
                .Take(RepositoryConstants.PageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false);
        }


        public async Task<int> InsertYearsAsync(Year model, CancellationToken cancellationToken)
        {
            await _context.Years.AddAsync(model, cancellationToken).ConfigureAwait(false);
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
