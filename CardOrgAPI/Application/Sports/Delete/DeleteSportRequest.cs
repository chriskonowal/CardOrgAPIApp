using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.Sports.Delete
{
    public class DeleteSportRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
