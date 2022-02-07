using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Cards.Carousel
{
    public class CardCarouselRequest : IRequest<ApiResponse<IEnumerable<CardResponse>>>
    {
        public int Count { get; set; }
    }
}
