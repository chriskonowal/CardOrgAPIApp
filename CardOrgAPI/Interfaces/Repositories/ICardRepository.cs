using CardOrgAPI.Entities;
using CardOrgAPI.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface ICardRepository 
    {
        Task<IEnumerable<Card>> GetCardsByAmountAsync(int count, CancellationToken cancellationToken);

        Task<IEnumerable<Card>> GetAsync(CardSearchQueryFilter filter, CancellationToken cancellationToken);

        int GetTotal(CardSearchQueryFilter filter);
        Task<bool> InsertAsync(CardQueryFilter queryFilter, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
