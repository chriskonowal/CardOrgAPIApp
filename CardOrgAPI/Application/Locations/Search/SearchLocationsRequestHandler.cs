using CardOrgAPI.Converters;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using CardOrgAPI.QueryFilters;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Locations.Search
{
    public class SearchLocationsRequestHandler : IRequestHandler<SearchLocationsRequest, ApiResponse<SearchLocationsResponse>>
    {
        private readonly ILocationRepository _locationRepository;

        public SearchLocationsRequestHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<ApiResponse<SearchLocationsResponse>> Handle(SearchLocationsRequest request, CancellationToken cancellationToken)
        {
            var queryFilter = new GenericSearchQueryFilter
            {
                IsSortDesc = request.IsSortDesc,
                PageNumber = request.PageNumber,
                RowsPerPage = request.RowsPerPage,
                SearchTerm = request.SearchTerm,
                SortByField = request.SortByField
            };

            var models = await _locationRepository.GetAsync(queryFilter,
                cancellationToken).ConfigureAwait(false);

            var total = _locationRepository.GetTotal(request.SearchTerm);

            var response = new ApiResponse<SearchLocationsResponse>()
            {
                Value = new SearchLocationsResponse()
                {
                    Locations = models.Select(x => LocationConverter.Convert(x)),
                    Total = total
                },
                IsSuccessful = true
            };

            return response;
        }
    }
}
