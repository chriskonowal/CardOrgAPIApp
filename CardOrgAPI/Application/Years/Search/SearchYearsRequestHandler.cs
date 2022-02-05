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

namespace CardOrgAPI.Application.Years.Search
{
    public class SearchYearsRequestHandler : IRequestHandler<SearchYearsRequest, ApiResponse<SearchYearsResponse>>
    {
        private readonly IYearRepository _yearRepository;

        public SearchYearsRequestHandler(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        public async Task<ApiResponse<SearchYearsResponse>> Handle(SearchYearsRequest request, CancellationToken cancellationToken)
        {

            var queryFilter = new GetYearsQueryFilter()
            {
                IsSortDesc = request.IsSortDesc,
                PageNumber = request.PageNumber,
                RowsPerPage = request.RowsPerPage,
                SearchYear = request.SearchYear,
                SortByField = request.SortByField
            };

            var years = await _yearRepository.GetYearsAsync(queryFilter,
                cancellationToken).ConfigureAwait(false);

            var totalYears = _yearRepository.GetYearsTotal(request.SearchYear);

            var response = new ApiResponse<SearchYearsResponse>()
            {
                Value = new SearchYearsResponse()
                {
                    Years = years.Select(x => YearConverter.Convert(x)),
                    TotalYears = totalYears
                },
                IsSuccessful = true
            };

            return response;
        }
    }
}
