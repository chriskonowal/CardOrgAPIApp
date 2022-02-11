using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Locations.Delete
{
    public class DeleteLocationRequest : IRequest<ApiResponse>
    {
        public int Id { get; set; }
    }
}
