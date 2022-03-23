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

namespace CardOrgAPI.Application.Sports.Search
{
    public class SearchSportsRequestHandler : IRequestHandler<SearchSportsRequest, ApiResponse<SearchSportsResponse>>
    {
        private readonly ISportRepository _sportRepository;

        public SearchSportsRequestHandler(ISportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }

        public async Task<ApiResponse<SearchSportsResponse>> Handle(SearchSportsRequest request, CancellationToken cancellationToken)
        {
            var queryFilter = new GenericSearchQueryFilter
            {
                IsSortDesc = request.IsSortDesc,
                PageNumber = request.PageNumber,
                RowsPerPage = request.RowsPerPage,
                SearchTerm = request.SearchTerm,
                SortByField = request.SortByField
            };

            var models = await _sportRepository.GetAsync(queryFilter,
                cancellationToken).ConfigureAwait(false);

            var total = _sportRepository.GetTotal(request.SearchTerm);

            var response = new ApiResponse<SearchSportsResponse>()
            {
                Value = new SearchSportsResponse()
                {
                    Sports = models.Select(x => SportConverter.Convert(x)),
                    Total = total
                },
                IsSuccessful = true
            };

            return response;
        }
    }
}
