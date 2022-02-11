using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years.Delete
{
    public class DeleteYearRequestHandler : IRequestHandler<DeleteYearRequest, ApiResponse>
    {
        private readonly IYearRepository _yearRepository;

        public DeleteYearRequestHandler(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        public async Task<ApiResponse> Handle(DeleteYearRequest request, CancellationToken cancellationToken)
        {
            var result = await _yearRepository.DeleteAsync(request.YearId, cancellationToken).ConfigureAwait(false);
            var response = new ApiResponse();
            response.IsSuccessful = result;
            return response;
        }
    }
}
