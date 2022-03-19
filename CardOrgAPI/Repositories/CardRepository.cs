using CardOrgAPI.Contexts;
using CardOrgAPI.Helpers;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using CardOrgAPI.QueryFilters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly CardOrgContext _context;
        private bool disposed = false;

        public CardRepository(CardOrgContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Card>> GetCardsByAmountAsync(int count, CancellationToken cancellationToken)
        {
            var query = _context.Card.AsQueryable();
            query = IncludeAllModels(query)
                    .Where(x => !String.IsNullOrWhiteSpace(x.FrontCardMainImagePath) &&
                    !String.IsNullOrWhiteSpace(x.BackCardMainImagePath))
                    .OrderByDescending(x => x.TimeStamp).Take(count);

            return await query.ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Card>> GetAsync(CardSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Card.AsQueryable();
            query = IncludeAllModels(query);
            query = QuickSearch(query, filter.QuickSearchTerm);
            query = FullSearchSort(query, filter.SearchSortQueryFilter);
            query = RepositoryHelpers.Paging(query, filter.RowsPerPage, filter.PageNumber);          
            try
            {
                return await Task.FromResult(query.ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public int GetTotal(CardSearchQueryFilter filter)
        {
            var query = _context.Card.AsQueryable();
            query = IncludeAllModels(query);
            query = QuickSearch(query, filter.QuickSearchTerm);
            query = FullSearchSort(query, filter.SearchSortQueryFilter);
            return query.Count();
        }

        private IQueryable<Card> IncludeAllModels(IQueryable<Card> query)
        {
            query = query.Include(x => x.PlayerCards).ThenInclude(x => x.Player);
            query = query.Include(x => x.TeamCards).ThenInclude(x => x.Team);
            return query.Include(x => x.GradeCompany)
                    .Include(x => x.Location)
                    .Include(x => x.Set)
                    .Include(x => x.Sport)
                    .Include(x => x.Year)
                    .AsQueryable();
        }

        private IQueryable<Card> QuickSearch(IQueryable<Card> cards, string quickSearch)
        {
            quickSearch = quickSearch.ToLower();
            if (!String.IsNullOrWhiteSpace(quickSearch))
            {
                cards = cards.Where(x => x.CardDescription.ToLower().Contains(quickSearch) || 
                x.CardNumber.ToLower().Contains(quickSearch) || 
                x.GradeCompany.Name.ToLower().Contains(quickSearch) || 
                x.Location.Name.ToLower().Contains(quickSearch) ||
                x.PlayerCards.Any(x => x.Player.FirstName.ToLower().Contains(quickSearch) || 
                x.Player.LastName.ToLower().Contains(quickSearch)) ||
                x.Set.Name.ToLower().Contains(quickSearch) || 
                x.Sport.Name.ToLower().Contains(quickSearch) || 
                x.TeamCards.Any(x => x.Team.City.ToLower().Contains(quickSearch) ||
                x.Team.Name.ToLower().Contains(quickSearch)));
            }
            return cards;
        }

        private IQueryable<Card> FullSearchSort(IQueryable<Card> cards, SearchSortQueryFilter filter )
        {
            bool hasCustomOrder = false;
            if (!String.IsNullOrWhiteSpace(filter.CardDescription))
            {
                cards = cards.Where(x => x.CardDescription.ToLower().Contains(filter.CardDescription.ToLower()));
            }

            if (!String.IsNullOrWhiteSpace(filter.YearIds)) 
            {
                var years = filter.YearIds.Split(",").Select(x => int.Parse(x));
                cards = cards.Where(x => years.Contains(x.Year.YearId));
            }

            if (filter.IsGraded)
            {
                cards = cards.Where(x => x.IsGraded);
            }

            if (filter.PlayerNameSort > 0)
            {
                hasCustomOrder = true;
                if (filter.PlayerNameSort == 1)
                {
                    cards = cards.OrderBy(x => x.PlayerCards.Select(p => p.Player).OrderBy(y => y.LastName).ThenBy(y => y.FirstName).FirstOrDefault().LastName).ThenBy(x => x.PlayerCards.Select(p => p.Player).OrderBy(y => y.LastName).ThenBy(y => y.FirstName).FirstOrDefault().FirstName).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.PlayerCards.Select(p => p.Player).OrderByDescending(y => y.LastName).ThenByDescending(y => y.FirstName).FirstOrDefault().LastName).ThenByDescending(x => x.PlayerCards.Select(p => p.Player).OrderByDescending(y => y.LastName).ThenByDescending(y => y.FirstName).FirstOrDefault().FirstName).ToList().AsQueryable();
                }
            }

            if (filter.TeamSort > 0)
            {
                hasCustomOrder = true;
                if (filter.TeamSort == 1)
                {
                    cards = cards.OrderBy(x => x.TeamCards.Select(p => p.Team).OrderBy(y => y.City).ThenBy(y => y.Name).FirstOrDefault().City).ThenBy(x => x.TeamCards.Select(p => p.Team).OrderBy(y => y.Name).ThenBy(y => y.Name).FirstOrDefault().Name).ToList().AsQueryable();
                }
                else
                {
                    cards = cards.OrderByDescending(x => x.TeamCards.Select(p => p.Team).OrderByDescending(y => y.City).ThenByDescending(y => y.Name).FirstOrDefault().City).ThenByDescending(x => x.TeamCards.Select(p => p.Team).OrderByDescending(y => y.Name).ThenByDescending(y => y.Name).FirstOrDefault().Name).ToList().AsQueryable();
                }
            }

            if (!hasCustomOrder)
            {
                cards = cards.OrderByDescending(x => x.TimeStamp);
            }

            return cards;
        }

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
