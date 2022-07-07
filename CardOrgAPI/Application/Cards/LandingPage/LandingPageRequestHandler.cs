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

namespace CardOrgAPI.Application.Cards.LandingPage
{
    public class LandingPageRequestHandler : IRequestHandler<LandingPageRequest, ApiResponse<LandingPageResponse>>
    {
        private readonly ICardRepository _cardRepository;

        public LandingPageRequestHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<ApiResponse<LandingPageResponse>> Handle(LandingPageRequest request, CancellationToken cancellationToken)
        {
            var queryFilter = new CardSearchQueryFilter()
            {
                PageNumber = request.PageNumber,
                RowsPerPage = request.RowsPerPage,
                QuickSearchTerm = request.QuickSearch,
                SearchSortQueryFilter = SearchSortQueryFilterConverter.Convert(request.SearchSortRequest),
                IsSortDesc = request.IsSortDesc,
                SortByField = request.SortByField
            };


            var cards = await _cardRepository.GetAsync(queryFilter,
                cancellationToken).ConfigureAwait(false);

            var totalCards = _cardRepository.GetTotal(queryFilter);

            var response = new ApiResponse<LandingPageResponse>()
            {
                Value = new LandingPageResponse()
                {
                    Cards = cards.Select(x => CardConverter.Convert(x)),
                    TotalCards = totalCards
                },
                IsSuccessful = true
            };

            return response;
        }
    }
}
