using CardOrgAPI.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface IYearRepository : IDisposable
    {
        Task<IEnumerable<Year>> GetYearsAsync(CancellationToken cancellationToken);

        Task<int> InsertYearsAsync(Year model, CancellationToken cancellationToken);
    }
}
