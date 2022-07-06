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
    public class TeamRepository : ITeamRepository
    {
        private readonly CardOrgContext _context;

        public TeamRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Teams.AsQueryable();
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
            var query = _context.Teams.AsQueryable();
            query = Search(query, searchTerm);
            return query.Count();
        }

        public bool Exists(string city, string name)
        {
            return _context.Teams
                .Any(x => x.City.ToLower().Contains(city.ToLower())
                && x.Name.ToLower().Contains(name.ToLower()));
        }

        public async Task<bool> InsertAsync(Team model, CancellationToken cancellationToken)
        {
            await _context.Teams.AddAsync(model, cancellationToken).ConfigureAwait(false);
            if (model.TeamId > 0)
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
                var year = new Team { TeamId = id };
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
