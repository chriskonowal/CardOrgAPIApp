using CardOrgAPI.Entities;
using CardOrgAPI.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface IYearRepository
    {
        Task<IEnumerable<Year>> GetAsync(GetYearsQueryFilter filter, CancellationToken cancellationToken);
        int GetTotal(int searchYear);
        bool Exists(int beginningYear, int endingYear);
        Task<bool> InsertAsync(Year model, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
