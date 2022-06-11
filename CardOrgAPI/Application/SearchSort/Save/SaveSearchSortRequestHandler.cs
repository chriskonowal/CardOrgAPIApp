using CardOrgAPI.Converters;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.SearchSort.Save
{
    public class SaveSearchSortRequestHandler : IRequestHandler<SaveSearchSortRequest, ApiResponse>
    {
        private readonly ISearchSortRepository _searchSortRepository;

        public SaveSearchSortRequestHandler(ISearchSortRepository searchSortRepository)
        {
            _searchSortRepository = searchSortRepository;
        }

        public async Task<ApiResponse> Handle(SaveSearchSortRequest request, CancellationToken cancellationToken)
        {
            var queryFilter = SearchSortQueryFilterConverter.Convert(request.SearchSortRequest);
            await _searchSortRepository.SaveAsync(queryFilter, cancellationToken).ConfigureAwait(false);
            return new ApiResponse()
            {
                ErrorMessage = "",
                IsSuccessful = true
            };
        }
    }
}
