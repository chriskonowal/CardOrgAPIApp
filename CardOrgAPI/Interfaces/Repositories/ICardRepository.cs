using CardOrgAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface ICardRepository :IDisposable
    {
        Task<IEnumerable<Card>> GetCardsByAmountAsync(int count, CancellationToken cancellationToken);
    }
}
