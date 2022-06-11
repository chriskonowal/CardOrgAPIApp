﻿using CardOrgAPI.QueryFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Interfaces.Repositories
{
    public interface ISearchSortRepository
    {
        Task<bool> SaveAsync(SearchSortQueryFilter queryFilter, CancellationToken cancellationToken);
    }
}
