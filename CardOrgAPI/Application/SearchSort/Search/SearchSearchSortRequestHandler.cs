using CardOrgAPI.Converters;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.QueryFilters;
using CardOrgAPI.Responses;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.SearchSort.Search
{
    public class SearchSearchSortRequestHandler : IRequestHandler<SearchSearchSortRequest, ApiResponse<SearchSearchSortResponse>>
    {
        private readonly ISearchSortRepository _searchSortRepository;

        public SearchSearchSortRequestHandler(ISearchSortRepository searchSortRepository)
        {
            _searchSortRepository = searchSortRepository;
        }

        public async Task<ApiResponse<SearchSearchSortResponse>> Handle(SearchSearchSortRequest request, CancellationToken cancellationToken)
        {
            var queryFilter = new GenericSearchQueryFilter
            {
                IsSortDesc = request.IsSortDesc,
                PageNumber = request.PageNumber,
                RowsPerPage = request.RowsPerPage,
                SearchTerm = request.SearchTerm,
                SortByField = request.SortByField
            };

            var models = await _searchSortRepository.GetAsync(queryFilter,
                cancellationToken).ConfigureAwait(false);

            var total = _searchSortRepository.GetTotal(request.SearchTerm);

            var response = new ApiResponse<SearchSearchSortResponse>()
            {
                Value = new SearchSearchSortResponse()
                {
                    SearchSorts = models.Select(x => SearchSortConverter.Convert(x)),
                    Total = total
                },
                IsSuccessful = true
            };

            return response;
        }
    }
}
