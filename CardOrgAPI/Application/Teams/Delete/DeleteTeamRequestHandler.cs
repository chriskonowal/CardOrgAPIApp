using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Teams.Delete
{
    public class DeleteTeamRequestHandler : IRequestHandler<DeleteTeamRequest, ApiResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public DeleteTeamRequestHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<ApiResponse> Handle(DeleteTeamRequest request, CancellationToken cancellationToken)
        {
            var result = await _teamRepository.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
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
