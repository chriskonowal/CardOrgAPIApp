using CardOrgAPI.Contexts;
using CardOrgAPI.Model;
using CardOrgAPI.QueryFilters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Repositories
{
    public class SearchSortRepository
    {
        private readonly CardOrgContext _context;

        public SearchSortRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveAsync(SearchSort model, CancellationToken cancellationToken)
        {
            await _context.SearchSort.AddAsync(model, cancellationToken).ConfigureAwait(false);
            if (model.SearchSortId > 0)
            {
                _context.Entry(model).State = EntityState.Modified;
            }
            var result = await _context.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }
    }
}
