using CardOrgAPI.Model;
using CardOrgAPI.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface ISetRepository
    {
        Task<IEnumerable<Set>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken);

        int GetTotal(string searchTerm);
    }
}
