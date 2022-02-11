using CardOrgAPI.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Locations.Save
{
    public class SaveLocationRequest : IRequest<ApiResponse>
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
}
