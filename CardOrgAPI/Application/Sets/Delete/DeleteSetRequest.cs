using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.Sets.Delete
{
    public class DeleteSetRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
