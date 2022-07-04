using CardOrgAPI.Entities;
using CardOrgAPI.Helpers;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.QueryFilters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Repositories
{
    public class SetRepository : ISetRepository
    {
        private readonly CardOrgContext _context;

        public SetRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Set>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Sets.AsQueryable();
            query = Search(query, filter.SearchTerm);

            if (!String.IsNullOrWhiteSpace(filter.SortByField))
            {
                if (filter.SortByField.ToLower().Equals("name"))
                {
                    if (filter.IsSortDesc)
                    {
                        query = query.OrderByDescending(x => x.Name);
                    }
                    else
                    {
                        query = query.OrderBy(x => x.Name);
                    }
                }
            }
            else
            {
                query = query.OrderBy(x => x.Name);
            }

            query = RepositoryHelpers.Paging(query, filter.RowsPerPage, filter.PageNumber);

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public int GetTotal(string searchTerm)
        {
            var query = _context.Sets.AsQueryable();
            query = Search(query, searchTerm);
            return query.Count();
        }

        private IQueryable<Set> Search(IQueryable<Set> query, string searchTerm)
        {
            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            return query;
        }

        public bool Exists(string searchTerm)
        {
            return _context.Sets
                .Any(x => x.Name.ToLower().Contains(searchTerm.ToLower()));

        }

        public async Task<bool> InsertAsync(Set model, CancellationToken cancellationToken)
        {
            await _context.Sets.AddAsync(model, cancellationToken).ConfigureAwait(false);
            if (model.SetId > 0)
            {
                _context.Entry(model).State = EntityState.Modified;
            }
            var result = await _context.SaveChangesAsync().ConfigureAwait(false);
            return result == 1;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (id > 0)
            {
                var year = new Set { SetId = id };
                if (_context.Entry(year) == null)
                {
                    return false;
                }
                _context.Entry(year).State = EntityState.Deleted;
                var result = await _context.SaveChangesAsync(cancellationToken);
                return result == 1;
            }
            return true;
        }
    }
}
