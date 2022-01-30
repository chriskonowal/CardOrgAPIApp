using CardOrgAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface IYearRepository : IDisposable
    {
        Task<IEnumerable<Year>> GetYearsAsync(int page, CancellationToken cancellationToken);

        Task<IEnumerable<Year>> SearchYearsAsync(int year, int page, CancellationToken cancellationToken);

        Task<int> InsertYearsAsync(Year model, CancellationToken cancellationToken);
    }
}
