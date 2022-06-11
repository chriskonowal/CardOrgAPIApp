using CardOrgAPI.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CardOrgAPI.Entities;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken);

        int GetTotal(string searchTerm);

    }
}
