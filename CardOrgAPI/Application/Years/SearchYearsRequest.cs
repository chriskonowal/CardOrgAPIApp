using CardOrgAPI.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years
{
    public class SearchYearsRequest : IRequest<IEnumerable<Year>>
    {
        public int Page { get; set; }

        public int SearchYear { get; set; }
    }
}
