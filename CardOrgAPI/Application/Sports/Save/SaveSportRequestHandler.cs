using CardOrgAPI.Entities;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Sports.Save
{
    public class SaveSportRequestHandler : IRequestHandler<SaveSportRequest, ApiResponse>
    {
        private readonly ISportRepository _sportRepository;

        public SaveSportRequestHandler(ISportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }

        public async Task<ApiResponse> Handle(SaveSportRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (request.Id <= 0)
            {
                var exists = _sportRepository.Exists(request.Name);
                if (exists)
                {
                    response.ErrorMessage = "Sport entry already exists";
                    response.IsSuccessful = false;
                    return response;
                }
            }

            var model = new Sport()
            {
                Name = request.Name,
                SportId = request.Id > 0 ? request.Id : 0
            };

            var insertSucessful = await _sportRepository.InsertAsync(model, cancellationToken).ConfigureAwait(false);
            if (insertSucessful)
            {
                response.IsSuccessful = true;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "Something happened. Sport was not added.";
            }

            return response;
        }
    }
}
