using CardOrgAPI.Contexts;
using CardOrgAPI.Helpers;
using CardOrgAPI.Interfaces.Repositories;
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
    public class PlayerRepository : IPlayerRepository
    {
        private readonly CardOrgContext _context;

        public PlayerRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Player.AsQueryable();
            query = Search(query, filter.SearchTerm);

            if (!String.IsNullOrWhiteSpace(filter.SortByField))
            {
                if (filter.SortByField.ToLower().Equals("firstName"))
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
                else if (filter.SortByField.ToLower().Equals("lastName"))
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
            var query = _context.Player.AsQueryable();
            query = Search(query, searchTerm);
            return query.Count();
        }

        private IQueryable<Player> Search(IQueryable<Player> teams, string searchTerm)
        {
            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                teams = teams.Where(x => x.LastName.ToLower().Contains(searchTerm.ToLower()) ||
                x.FirstName.ToLower().Contains(searchTerm.ToLower()));
            }
            return teams;
        }
    }
}
