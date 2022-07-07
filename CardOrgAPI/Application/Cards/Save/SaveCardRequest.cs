using CardOrgAPI.Requests;
using CardOrgAPI.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CardOrgAPI.Application.Cards.Save
{
    public class SaveCardRequest : IRequest<ApiResponse>
    {
        public CardRequest CardRequest { get; set; }
    }
}
