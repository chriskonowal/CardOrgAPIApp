using CardOrgAPI.Entities;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Sets.Save
{
    public class SaveSetRequestHandler : IRequestHandler<SaveSetRequest, ApiResponse>
    {
        private readonly ISetRepository _setRepository;

        public SaveSetRequestHandler(ISetRepository setRepository)
        {
            _setRepository = setRepository;
        }

        public async Task<ApiResponse> Handle(SaveSetRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (request.Id <= 0)
            {
                var exists = _setRepository.Exists(request.Name);
                if (exists)
                {
                    response.ErrorMessage = "Set entry already exists";
                    response.IsSuccessful = false;
                    return response;
                }
            }

            var model = new Set()
            {
                Name = request.Name,
                SetId = request.Id > 0 ? request.Id : 0
            };

            var insertSucessful = await _setRepository.InsertAsync(model, cancellationToken).ConfigureAwait(false);
            if (insertSucessful)
            {
                response.IsSuccessful = true;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "Something happened. Set was not added.";
            }

            return response;
        }
    }
}
