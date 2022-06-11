using CardOrgAPI.Constants;
using CardOrgAPI.Entities;
using CardOrgAPI.Extensions;
using CardOrgAPI.Helpers;
using CardOrgAPI.Interfaces.Repositories;
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

        public async Task<IEnumerable<Year>> GetAsync(GetYearsQueryFilter filter, CancellationToken cancellationToken)
        {
            try
            {
                var query = _context.Years.AsQueryable();
                query = Search(query, filter.SearchYear);

                if (!String.IsNullOrWhiteSpace(filter.SortByField))
                {
                    if (filter.SortByField.ToLower().Equals("beginningyear"))
                    {
                        if (filter.IsSortDesc)
                        {
                            query = query.OrderByDescending(x => x.BeginningYear);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.BeginningYear);
                        }
                    }
                    else if (filter.SortByField.ToLower().Equals("endingyear"))
                    {
                        if (filter.IsSortDesc)
                        {
                            query = query.OrderByDescending(x => x.EndingYear);
                        }
                        else
                        {
                            query = query.OrderBy(x => x.EndingYear);
                        }
                    }
                }
                else
                {
                    query = query.OrderByDescending(x => x.EndingYear);
                }

                query = RepositoryHelpers.Paging(query, filter.RowsPerPage, filter.PageNumber);

                return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetTotal(int searchYear)
        {
            var query = _context.Years.AsQueryable();
            query = Search(query, searchYear);
            return query.Count();
        }

        public bool Exists(int beginningYear, int endingYear)
        {
            return _context.Years
                .Any(x => x.BeginningYear == beginningYear && x.EndingYear == endingYear);

        }

        public async Task<bool> InsertAsync(Year model, CancellationToken cancellationToken)
        {
            await _context.Years.AddAsync(model, cancellationToken).ConfigureAwait(false);
            if (model.YearId > 0)
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
                var year = new Year { YearId = id };
                _context.Entry(year).State = EntityState.Deleted;
                var result = await _context.SaveChangesAsync(cancellationToken);
                return result == 1;
            }
            return true;
        }

        private IQueryable<Year> Search(IQueryable<Year> years, int searchYear)
        {
            if (searchYear > 0)
            {
                years = years.Where(x => x.BeginningYear == searchYear || x.EndingYear == searchYear);
            }
            return years;
        }
    }
}
