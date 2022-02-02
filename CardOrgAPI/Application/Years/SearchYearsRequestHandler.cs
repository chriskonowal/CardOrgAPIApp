using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years
{
    public class SearchYearsRequestHandler : IRequestHandler<SearchYearsRequest, IEnumerable<Year>>
    {
        private readonly IYearRepository _yearRepository;

        public SearchYearsRequestHandler(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        public async Task<IEnumerable<Year>> Handle(SearchYearsRequest request, CancellationToken cancellationToken)
        {
            return await _yearRepository.SearchYearsAsync(request.SearchYear, 
                request.Page, 
                cancellationToken).ConfigureAwait(false);
            
            throw new NotImplementedException();
        }
    }
}
