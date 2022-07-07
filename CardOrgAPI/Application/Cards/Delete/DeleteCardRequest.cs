using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.Cards.Delete
{
    public class DeleteCardRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
