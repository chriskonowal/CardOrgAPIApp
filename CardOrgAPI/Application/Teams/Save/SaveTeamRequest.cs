using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.Teams.Save
{
    public class SaveTeamRequest : IRequest<ApiResponse>
    {
        public string City { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }
    }
}
