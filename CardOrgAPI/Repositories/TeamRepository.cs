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
    public class TeamRepository : ITeamRepository
    {
        private readonly CardOrgContext _context;

        public TeamRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Team.AsQueryable();
            query = Search(query, filter.SearchTerm);

            if (!String.IsNullOrWhiteSpace(filter.SortByField))
            {
                if (filter.SortByField.ToLower().Equals("city"))
                {
                    if (filter.IsSortDesc)
                    {
                        query = query.OrderByDescending(x => x.City).ThenByDescending(x => x.Name);
                    }
                    else
                    {
                        query = query.OrderBy(x => x.City).ThenBy(x => x.Name);
                    }
                }
                else if (filter.SortByField.ToLower().Equals("name"))
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
                query = query.OrderBy(x => x.City).ThenBy(x => x.Name);
            }

            query = RepositoryHelpers.Paging(query, filter.RowsPerPage, filter.PageNumber);

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public int GetTotal(string searchTerm)
        {
            var query = _context.Team.AsQueryable();
            query = Search(query, searchTerm);
            return query.Count();
        }

        private IQueryable<Team> Search(IQueryable<Team> teams, string searchTerm)
        {
            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                teams = teams.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || 
                x.City.ToLower().Contains(searchTerm.ToLower()));
            }
            return teams;
        }
    }
}
