using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Locations.Delete
{
    public class DeleteLocationRequestHandler : IRequestHandler<DeleteLocationRequest, ApiResponse>
    {
        private readonly ILocationRepository _locationRepository;

        public DeleteLocationRequestHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<ApiResponse> Handle(DeleteLocationRequest request, CancellationToken cancellationToken)
        {
            var result = await _locationRepository.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
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
