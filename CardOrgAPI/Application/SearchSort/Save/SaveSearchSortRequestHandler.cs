using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.SearchSort.Save
{
    public class SaveSearchSortRequestHandler : IRequestHandler<SaveSearchSortRequest, ApiResponse>
    {
        public Task<ApiResponse> Handle(SaveSearchSortRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
