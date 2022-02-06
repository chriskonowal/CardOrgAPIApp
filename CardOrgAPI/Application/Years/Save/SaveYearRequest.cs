using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years.Save
{
    public class SaveYearRequest : IRequest<ApiResponse>
    {
        public int BeginningYear { get; set; }

        public int EndingYear { get; set; }

        public int YearId { get; set; }
    }
}
