using CardOrgAPI.Entities;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Locations.Save
{
    public class SaveLocationRequestHandler : IRequestHandler<SaveLocationRequest, ApiResponse>
    {
        private readonly ILocationRepository _locationRepository;

        public SaveLocationRequestHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<ApiResponse> Handle(SaveLocationRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (request.Id <= 0)
            {
                var exists = _locationRepository.Exists(request.Name);
                if (exists)
                {
                    response.ErrorMessage = "Location entry already exists";
                    response.IsSuccessful = false;
                    return response;
                }
            }


            var model = new Location() {
                Name = request.Name,
                LocationId = request.Id > 0 ? request.Id : 0
            };

            var insertSucessful = await _locationRepository.InsertAsync(model, cancellationToken).ConfigureAwait(false);
            if (insertSucessful)
            {
                response.IsSuccessful = true;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "Something happened. Location was not added.";
            }

            return response;
        }
    }
}
