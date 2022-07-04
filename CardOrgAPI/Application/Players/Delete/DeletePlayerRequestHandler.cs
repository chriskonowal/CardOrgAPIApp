using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Players.Delete
{
    public class DeletePlayerRequestHandler : IRequestHandler<DeletePlayerRequest, ApiResponse>
    {
        private readonly IPlayerRepository _playerRepository;

        public DeletePlayerRequestHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<ApiResponse> Handle(DeletePlayerRequest request, CancellationToken cancellationToken)
        {
            var result = await _playerRepository.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
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
