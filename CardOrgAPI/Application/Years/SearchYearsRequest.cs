﻿using CardOrgAPI.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardOrgAPI.Application.Years
{
    public class SearchYearsRequest : IRequest<SearchYearsResponse>
    {
        public int SearchYear { get; set; }
        public int RowsPerPage { get; set; }
        public int PageNumber { get; set; }

        public string SortByField { get; set; }

        public bool IsSortDesc { get; set; }
    }
}
