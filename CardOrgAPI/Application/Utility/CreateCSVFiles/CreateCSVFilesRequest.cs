using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Utility.CreateCSVFiles
{
    public class CreateCSVFilesRequest : IRequest<ApiResponse<CreateCSVFilesResponse>>
    {

    }
}
