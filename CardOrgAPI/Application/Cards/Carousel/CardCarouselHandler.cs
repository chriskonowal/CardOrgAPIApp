using CardOrgAPI.Converters;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Cards.Carousel
{
    public class CardCarouselHandler : IRequestHandler<CardCarouselRequest, ApiResponse<IEnumerable<CardResponse>>>
    {
        private readonly ICardRepository _cardRepository;

        public CardCarouselHandler(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        public async Task<ApiResponse<IEnumerable<CardResponse>>> Handle(CardCarouselRequest request, CancellationToken cancellationToken)
        {
            var models = await _cardRepository.GetCardsByAmountAsync(request.Count, cancellationToken).ConfigureAwait(false);
            var response = new ApiResponse<IEnumerable<CardResponse>>()
            {
                IsSuccessful = true,
                Value = models.Select(x => CardConverter.Convert(x))
            };
            return response;
        }
    }
}
