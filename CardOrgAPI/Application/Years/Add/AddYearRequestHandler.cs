using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years.Add
{
    public class AddYearRequestHandler : IRequestHandler<AddYearRequest, ApiResponse>
    {
        private readonly IYearRepository _yearRepository;

        public AddYearRequestHandler(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        public async Task<ApiResponse> Handle(AddYearRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            var yearExists = _yearRepository.YearExists(request.BeginningYear, request.EndingYear);
            if (yearExists)
            {
                response.ErrorMessage = "Year entry already exists";
                response.IsSuccessful = false;
                return response;
            }

            var model = new Year() {
                BeginningYear = request.BeginningYear,
                EndingYear = request.EndingYear,
                Cards = new Card[] { }
            };
            var insertYear = await _yearRepository.InsertYearsAsync(model, cancellationToken).ConfigureAwait(false);
            if (insertYear)
            {
                response.IsSuccessful = true;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "Something happened. Year was not added.";
            }

            return response;
        }
    }
}
