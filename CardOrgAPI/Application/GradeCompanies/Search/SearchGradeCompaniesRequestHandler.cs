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

namespace CardOrgAPI.Application.GradeCompanies.Search
{
    public class SearchGradeCompaniesRequestHandler : IRequestHandler<SearchGradeCompaniesRequest, ApiResponse<SearchGradeCompaniesResponse>>
    {
        private readonly IGradeCompanyRepository _gradeCompanyRepository;

        public SearchGradeCompaniesRequestHandler(IGradeCompanyRepository gradeCompanyRepository)
        {
            _gradeCompanyRepository = gradeCompanyRepository;
        }

        public async Task<ApiResponse<SearchGradeCompaniesResponse>> Handle(SearchGradeCompaniesRequest request, CancellationToken cancellationToken)
        {
            var queryFilter = new GenericSearchQueryFilter
            {
                IsSortDesc = request.IsSortDesc,
                PageNumber = request.PageNumber,
                RowsPerPage = request.RowsPerPage,
                SearchTerm = request.SearchTerm,
                SortByField = request.SortByField
            };

            var models = await _gradeCompanyRepository.GetAsync(queryFilter,
                cancellationToken).ConfigureAwait(false);

            var total = _gradeCompanyRepository.GetTotal(request.SearchTerm);

            var response = new ApiResponse<SearchGradeCompaniesResponse>()
            {
                Value = new SearchGradeCompaniesResponse()
                {
                    GradeCompanies = models.Select(x => GradeCompanyConverter.Convert(x)),
                    Total = total
                },
                IsSuccessful = true
            };

            return response;
        }
    }
}
