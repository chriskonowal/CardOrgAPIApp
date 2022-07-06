using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.Sports.Save
{
    public class SaveSportRequest : IRequest<ApiResponse>
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
