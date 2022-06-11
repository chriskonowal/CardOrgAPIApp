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
    public class LocationRepository : ILocationRepository
    {
        private readonly CardOrgContext _context;

        public LocationRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Locations.AsQueryable();
            query = Search(query, filter.SearchTerm);
            if (!String.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                query = query.Where(x => x.Name.ToLower().Contains(filter.SearchTerm));
            }

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
                query = query.OrderByDescending(x => x.Name);
            }

            query = RepositoryHelpers.Paging(query, filter.RowsPerPage, filter.PageNumber);

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public int GetTotal(string searchTerm)
        {
            var query = _context.Locations.AsQueryable();
            query = Search(query, searchTerm);
            return query.Count();
        }

        public bool Exists(string searchTerm)
        {
            return _context.Locations
                .Any(x => x.Name.ToLower().Contains(searchTerm.ToLower()));

        }

        public async Task<bool> InsertAsync(Location model, CancellationToken cancellationToken)
        {
            await _context.Locations.AddAsync(model, cancellationToken).ConfigureAwait(false);
            if (model.LocationId > 0)
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
                var year = new Location { LocationId = id };
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

        private IQueryable<Location> Search(IQueryable<Location> locations, string searchTerm)
        {
            if (!String.IsNullOrWhiteSpace(searchTerm))
            {
                locations = locations.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            return locations;
        }
    }
}
