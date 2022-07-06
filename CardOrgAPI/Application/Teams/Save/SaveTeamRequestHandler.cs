using CardOrgAPI.Entities;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Teams.Save
{
    public class SaveTeamRequestHandler : IRequestHandler<SaveTeamRequest, ApiResponse>
    {
        private readonly ITeamRepository _teamRepository;

        public SaveTeamRequestHandler(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        public async Task<ApiResponse> Handle(SaveTeamRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (request.Id <= 0)
            {
                var exists = _teamRepository.Exists(request.City, request.Name);
                if (exists)
                {
                    response.ErrorMessage = "Team entry already exists";
                    response.IsSuccessful = false;
                    return response;
                }
            }

            var model = new Team()
            {
                City = request.City,
                Name = request.Name,
                TeamId = request.Id > 0 ? request.Id : 0
            };

            var insertSucessful = await _teamRepository.InsertAsync(model, cancellationToken).ConfigureAwait(false);
            if (insertSucessful)
            {
                response.IsSuccessful = true;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "Something happened. Team was not added.";
            }

            return response;
        }
    }
}
