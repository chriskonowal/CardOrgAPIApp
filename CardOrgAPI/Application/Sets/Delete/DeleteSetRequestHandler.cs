using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Sets.Delete
{
    public class DeleteSetRequestHandler : IRequestHandler<DeleteSetRequest, ApiResponse>
    {
        private readonly ISetRepository  _setRepository;

        public DeleteSetRequestHandler(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }
        public async Task<ApiResponse> Handle(DeleteSetRequest request, CancellationToken cancellationToken)
        {
            var result = await _setRepository.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
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
