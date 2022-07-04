using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.SearchSort.Delete
{
    public class DeleteSearchSortRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
