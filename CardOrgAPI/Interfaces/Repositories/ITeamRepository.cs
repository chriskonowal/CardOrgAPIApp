using CardOrgAPI.Entities;
using CardOrgAPI.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAsync(GenericSearchQueryFilter filter, CancellationToken cancellationToken);
        int GetTotal(string searchTerm);
        bool Exists(string city, string name);
        Task<bool> InsertAsync(Team model, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
