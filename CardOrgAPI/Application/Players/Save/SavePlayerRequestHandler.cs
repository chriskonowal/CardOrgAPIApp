using CardOrgAPI.Entities;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Players.Save
{
    public class SavePlayerRequestHandler : IRequestHandler<SavePlayerRequest, ApiResponse>
    {
        private readonly IPlayerRepository _playerRepository;

        public SavePlayerRequestHandler(IPlayerRepository playerRepository) {
            _playerRepository = playerRepository;
        }
        public async Task<ApiResponse> Handle(SavePlayerRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (request.Id <= 0)
            {
                var exists = _playerRepository.Exists(request.FirstName, request.LastName);
                if (exists)
                {
                    response.ErrorMessage = "Player entry already exists";
                    response.IsSuccessful = false;
                    return response;
                }
            }

            var model = new Player()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                PlayerId = request.Id > 0 ? request.Id : 0
            };

            var insertSucessful = await _playerRepository.InsertAsync(model, cancellationToken).ConfigureAwait(false);
            if (insertSucessful)
            {
                response.IsSuccessful = true;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "Something happened. Player was not added.";
            }

            return response;
        }
    }
}
