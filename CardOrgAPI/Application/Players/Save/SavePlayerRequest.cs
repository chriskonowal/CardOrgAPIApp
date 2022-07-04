using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.Players.Save
{
    public class SavePlayerRequest : IRequest<ApiResponse>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Id { get; set; }
    }
}
