using CardOrgAPI.Responses;
using MediatR;

namespace CardOrgAPI.Application.Sets.Save
{
    public class SaveSetRequest : IRequest<ApiResponse>
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
