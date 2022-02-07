using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.GradeCompanies.Save
{
    public class SaveGradeCompanyRequest : IRequest<ApiResponse>
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
