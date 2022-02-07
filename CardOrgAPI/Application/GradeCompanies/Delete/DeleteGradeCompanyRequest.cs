using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.GradeCompanies.Delete
{
    public class DeleteGradeCompanyRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
