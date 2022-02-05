using CardOrgAPI.Constants;
using CardOrgAPI.Contexts;
using CardOrgAPI.Extensions;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using CardOrgAPI.QueryFilters;
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

        public async Task<IEnumerable<Year>> GetYearsAsync(GetYearsQueryFilter filter, CancellationToken cancellationToken)
        {
            var years = _context.Years.AsQueryable();
            if (filter.SearchYear > 0)
            {
                years = years.Where(x => x.BeginningYear == filter.SearchYear || x.EndingYear == filter.SearchYear);
            }

            if (!String.IsNullOrWhiteSpace(filter.SortByField))
            {
                if (filter.SortByField.ToLower().Equals("beginningyear"))
                {
                    if (filter.IsSortDesc)
                    {
                        years = years.OrderByDescending(x => x.BeginningYear);
                    }
                    else
                    {
                        years = years.OrderBy(x => x.BeginningYear);
                    }
                }
                else if (filter.SortByField.ToLower().Equals("endingyear"))
                {
                    if (filter.IsSortDesc)
                    {
                        years = years.OrderByDescending(x => x.EndingYear);
                    }
                    else
                    {
                        years = years.OrderBy(x => x.EndingYear);
                    }
                }
            }
            else
            {
                years = years.OrderByDescending(x => x.EndingYear);
            }

            years = years.Skip((filter.PageNumber - 1) * filter.RowsPerPage).Take(filter.RowsPerPage);          
            return await years.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public int GetYearsTotal(int searchYear)
        {
            var years = _context.Years.AsQueryable();
            if (searchYear > 0)
            {
                years = years.Where(x => x.BeginningYear == searchYear || x.EndingYear == searchYear);
            }

            return years.Count();
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
