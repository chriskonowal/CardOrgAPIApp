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

namespace CardOrgAPI.Application.Players.Search
{
    public class SearchPlayersRequestHandler : IRequestHandler<SearchPlayersRequest, ApiResponse<SearchPlayersResponse>>
    {
        private readonly IPlayerRepository _playerRepository;

        public SearchPlayersRequestHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<ApiResponse<SearchPlayersResponse>> Handle(SearchPlayersRequest request, CancellationToken cancellationToken)
        {
            var queryFilter = new GenericSearchQueryFilter
            {
                IsSortDesc = request.IsSortDesc,
                PageNumber = request.PageNumber,
                RowsPerPage = request.RowsPerPage,
                SearchTerm = request.SearchTerm,
                SortByField = request.SortByField
            };

            var models = await _playerRepository.GetAsync(queryFilter,
                cancellationToken).ConfigureAwait(false);

            var total = _playerRepository.GetTotal(request.SearchTerm);

            var response = new ApiResponse<SearchPlayersResponse>()
            {
                Value = new SearchPlayersResponse()
                {
                    Players = models.Select(x => PlayerConverter.Convert(x)),
                    Total = total
                },
                IsSuccessful = true
            };

            return response;
        }
    }
}
