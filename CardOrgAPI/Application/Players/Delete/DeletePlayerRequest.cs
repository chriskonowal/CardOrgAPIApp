using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.Players.Delete
{
    public class DeletePlayerRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
