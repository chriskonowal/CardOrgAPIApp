using CardOrgAPI.Converters;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years.Load
{
    public class LoadYearRequestHandler : IRequestHandler<LoadYearRequest, ApiResponse<YearResponse>>
    {
        private readonly IYearRepository _yearRepository;

        public LoadYearRequestHandler(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        public async Task<ApiResponse<YearResponse>> Handle(LoadYearRequest request, CancellationToken cancellationToken)
        {
            var year = await _yearRepository.LoadYearAsync(request.YearId, cancellationToken).ConfigureAwait(false);
            var response = new ApiResponse<YearResponse>();

            if (year == null)
            {
                response.ErrorMessage = "Could not find year.";
                response.IsSuccessful = false;
            }

            response.Value = YearConverter.Convert(year);
            response.IsSuccessful = true;
            return response;
        }
    }
}
