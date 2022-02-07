using CardOrgAPI.Contexts;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            query = query.Include(x => x.PlayerCards).ThenInclude(x => x.Player);
            query = query.Include(x => x.TeamCards).ThenInclude(x => x.Team);
            query = query.Include(x => x.GradeCompany)
                    .Include(x => x.Location)
                    .Include(x => x.Set)
                    .Include(x => x.Sport)
                    .Include(x => x.Year)
                    .AsQueryable()
                    .Where(x => !String.IsNullOrWhiteSpace(x.FrontCardMainImagePath) &&
                    !String.IsNullOrWhiteSpace(x.BackCardMainImagePath))
                    .OrderByDescending(x => x.TimeStamp).Take(count);

            return await query.ToListAsync(cancellationToken)
                    .ConfigureAwait(false);
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
