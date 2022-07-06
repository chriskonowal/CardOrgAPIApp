using CardOrgAPI.Entities;
using CardOrgAPI.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface ISportRepository
    {
        Task<IEnumerable<Sport>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken);

        int GetTotal(string searchTerm);
        bool Exists(string searchTerm);
        Task<bool> InsertAsync(Sport model, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
