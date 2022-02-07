using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.GradeCompanies.Save
{
    public class SaveGradeCompanyRequestHandler : IRequestHandler<SaveGradeCompanyRequest, ApiResponse>
    {
        private readonly IGradeCompanyRepository _gradeCompanyRepository;

        public SaveGradeCompanyRequestHandler(IGradeCompanyRepository gradeCompanyRepository)
        {
            _gradeCompanyRepository = gradeCompanyRepository;
        }

        public async Task<ApiResponse> Handle(SaveGradeCompanyRequest request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse();
            if (request.Id <= 0)
            {
                var exists = _gradeCompanyRepository.Exists(request.Name);
                if (exists)
                {
                    response.ErrorMessage = "Grade Company entry already exists";
                    response.IsSuccessful = false;
                    return response;
                }
            }


            var model = new GradeCompany() {
                Name = request.Name,
                GradeCompanyId = request.Id > 0 ? request.Id : 0
            };

            var insertSucessful = await _gradeCompanyRepository.InsertAsync(model, cancellationToken).ConfigureAwait(false);
            if (insertSucessful)
            {
                response.IsSuccessful = true;
            }
            else
            {
                response.IsSuccessful = false;
                response.ErrorMessage = "Something happened. Grade Company was not added.";
            }

            return response;
        }
    }
}
