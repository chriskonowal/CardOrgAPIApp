using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.Teams.Delete
{
    public class DeleteTeamRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
