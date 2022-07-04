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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly CardOrgContext _context;

        public PlayerRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Players.AsQueryable();
            query = Search(query, filter.SearchTerm);

            if (!String.IsNullOrWhiteSpace(filter.SortByField))
            {
                if (filter.SortByField.ToLower().Equals("firstname"))
                {
                    if (filter.IsSortDesc)
                    {
                        query = query.OrderByDescending(x => x.FirstName).ThenByDescending(x => x.LastName);
                    }
                    else
                    {
                        query = query.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
                    }
                }
                else if (filter.SortByField.ToLower().Equals("lastname"))
                {
                    if (filter.IsSortDesc)
                    {
                        query = query.OrderByDescending(x => x.LastName).ThenByDescending(x => x.FirstName);
                    }
                    else
                    {
                        query = query.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
                    }
                }
            }
            else
            {
                query = query.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            }

            query = RepositoryHelpers.Paging(query, filter.RowsPerPage, filter.PageNumber);

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public int GetTotal(string searchTerm)
        {
            var query = _context.Players.AsQueryable();
            query = Search(query, searchTerm);
            return query.Count();
        }

        public bool Exists(string firstName, string lastName)
        {
            return _context.Players
                .Any(x => x.FirstName.ToLower().Contains(firstName.ToLower()) 
                && x.LastName.ToLower().Contains(lastName.ToLower()));
        }

        public async Task<bool> InsertAsync(Player model, CancellationToken cancellationToken)
        {
            await _context.Players.AddAsync(model, cancellationToken).ConfigureAwait(false);
            if (model.PlayerId > 0)
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
                var year = new Player { PlayerId = id };
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

        private IQueryable<Player> Search(IQueryable<Player> query, string searchTerm)
        {
            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(x => x.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                x.FirstName.ToLower().Contains(searchTerm.ToLower()));
            }
            return query;
        }
    }
}
