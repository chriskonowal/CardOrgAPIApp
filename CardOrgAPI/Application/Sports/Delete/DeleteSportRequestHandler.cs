using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Sports.Delete
{
    public class DeleteSportRequestHandler : IRequestHandler<DeleteSportRequest, ApiResponse>
    {
        private readonly ISportRepository _sportRepository;

        public DeleteSportRequestHandler(ISportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }
        public async Task<ApiResponse> Handle(DeleteSportRequest request, CancellationToken cancellationToken)
        {
            var result = await _sportRepository.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
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
