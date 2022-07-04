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

        bool Exists(string firstName, string lastName);
        Task<bool> InsertAsync(Player model, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

    }
}
