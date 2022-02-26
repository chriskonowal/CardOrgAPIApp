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
            var query = _context.Cards.AsQueryable();
            query = IncludeAllModels(query)
                    .Where(x => !String.IsNullOrWhiteSpace(x.FrontCardMainImagePath) &&
                    !String.IsNullOrWhiteSpace(x.BackCardMainImagePath))
                    .OrderByDescending(x => x.TimeStamp).Take(count);

            return await query.ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Card>> GetAsync(CardSearchQueryFilter filter, CancellationToken cancellationToken)
        {
            var query = _context.Cards.AsQueryable();
            query = IncludeAllModels(query);
            query = QuickSearch(query, filter.QuickSearchTerm);
            query = query.OrderByDescending(x => x.TimeStamp);
            query = RepositoryHelpers.Paging(query, filter.RowsPerPage, filter.PageNumber);          
            try
            {
                return await query.ToListAsync(cancellationToken)
                   .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public int GetTotal(string searchTerm)
        {
            var query = _context.Cards.AsQueryable();
            query = IncludeAllModels(query);
            query = QuickSearch(query, searchTerm);
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
