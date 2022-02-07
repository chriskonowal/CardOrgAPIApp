using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.GradeCompanies.Delete
{
    public class DeleteGradeCompanyRequestHandler : IRequestHandler<DeleteGradeCompanyRequest, ApiResponse>
    {
        private readonly IGradeCompanyRepository _gradeCompanyRepository;

        public DeleteGradeCompanyRequestHandler(IGradeCompanyRepository gradeCompanyRepository)
        {
            _gradeCompanyRepository = gradeCompanyRepository;
        }
        public async Task<ApiResponse> Handle(DeleteGradeCompanyRequest request, CancellationToken cancellationToken)
        {
            var result = await _gradeCompanyRepository.DeleteAsync(request.Id, cancellationToken).ConfigureAwait(false);
            var response = new ApiResponse();
            response.IsSuccessful = result;
            return response;
        }
    }
}
