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

namespace CardOrgAPI.Application.Years.Save
{
    public class SaveYearRequestHandler : IRequestHandler<SaveYearRequest, ApiResponse>
    {
        private readonly IYearRepository _yearRepository;

        public SaveYearRequestHandler(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        public async Task<ApiResponse> Handle(SaveYearRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (request.YearId <= 0)
            {
                var yearExists = _yearRepository.Exists(request.BeginningYear, request.EndingYear);
                if (yearExists)
                {
                    response.ErrorMessage = "Year entry already exists";
                    response.IsSuccessful = false;
                    return response;
                }
            }


            var model = new Year() {
                BeginningYear = request.BeginningYear,
                EndingYear = request.EndingYear,
                YearId = request.YearId > 0 ? request.YearId : 0,
                Cards = new Card[] { }
            };
            var insertYear = await _yearRepository.InsertAsync(model, cancellationToken).ConfigureAwait(false);
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
