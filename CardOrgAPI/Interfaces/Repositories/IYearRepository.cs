using CardOrgAPI.Model;
using CardOrgAPI.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface IYearRepository : IDisposable
    {
        Task<IEnumerable<Year>> GetYearsAsync(GetYearsQueryFilter filter, CancellationToken cancellationToken);
        int GetYearsTotal(int searchYear);
        bool YearExists(int beginningYear, int endingYear);
        Task<bool> InsertYearsAsync(Year model, CancellationToken cancellationToken);
        Task<Year> LoadYearAsync(int yearId, CancellationToken cancellation);
    }
}
