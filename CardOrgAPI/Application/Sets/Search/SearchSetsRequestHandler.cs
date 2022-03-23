using CardOrgAPI.Application.Sports.Search;
using CardOrgAPI.Converters;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.QueryFilters;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Sets.Search
{
    public class SearchSetsRequestHandler : IRequestHandler<SearchSetsRequest, ApiResponse<SearchSetsResponse>>
    {
        private readonly ISetRepository _setRepository;

        public SearchSetsRequestHandler(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public async Task<ApiResponse<SearchSetsResponse>> Handle(SearchSetsRequest request, CancellationToken cancellationToken)
        {
            var queryFilter = new GenericSearchQueryFilter
            {
                IsSortDesc = request.IsSortDesc,
                PageNumber = request.PageNumber,
                RowsPerPage = request.RowsPerPage,
                SearchTerm = request.SearchTerm,
                SortByField = request.SortByField
            };

            var models = await _setRepository.GetAsync(queryFilter,
                cancellationToken).ConfigureAwait(false);

            var total = _setRepository.GetTotal(request.SearchTerm);

            var response = new ApiResponse<SearchSetsResponse>()
            {
                Value = new SearchSetsResponse()
                {
                    Sets = models.Select(x => SetConverter.Convert(x)),
                    Total = total
                },
                IsSuccessful = true
            };

            return response;
        }
    }
}
