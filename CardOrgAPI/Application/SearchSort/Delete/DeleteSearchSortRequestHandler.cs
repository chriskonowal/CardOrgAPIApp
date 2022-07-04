using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.SearchSort.Delete
{
    public class DeleteSearchSortRequestHandler : IRequestHandler<DeleteSearchSortRequest, ApiResponse>
    {
        private readonly ISearchSortRepository _searchSortRepository;

        public DeleteSearchSortRequestHandler(ISearchSortRepository searchSortRepository)
        {
            _searchSortRepository = searchSortRepository;
        }

        public async Task<ApiResponse> Handle(DeleteSearchSortRequest request, CancellationToken cancellationToken)
        {
            var result = await _searchSortRepository.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
            var response = new ApiResponse();
            if (!result)
            {
                response.ErrorMessage = "Unable to delete entry.";
            }
            response.IsSuccessful = result;
            return response;
        }
    }
}
