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

namespace CardOrgAPI.Application.Teams.Search
{
    public class SearchTeamsRequestHandler : IRequestHandler<SearchTeamsRequest, ApiResponse<SearchTeamsResponse>>
    {
        private readonly ITeamRepository _teamRepository;

        public SearchTeamsRequestHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<ApiResponse<SearchTeamsResponse>> Handle(SearchTeamsRequest request, CancellationToken cancellationToken)
        {
            var queryFilter = new GenericSearchQueryFilter
            {
                IsSortDesc = request.IsSortDesc,
                PageNumber = request.PageNumber,
                RowsPerPage = request.RowsPerPage,
                SearchTerm = request.SearchTerm,
                SortByField = request.SortByField
            };

            var models = await _teamRepository.GetAsync(queryFilter,
                cancellationToken).ConfigureAwait(false);

            var total = _teamRepository.GetTotal(request.SearchTerm);

            var response = new ApiResponse<SearchTeamsResponse>()
            {
                Value = new SearchTeamsResponse()
                {
                    Teams = models.Select(x => TeamConverter.Convert(x)),
                    Total = total
                },
                IsSuccessful = true
            };

            return response;
        }
    }
}
