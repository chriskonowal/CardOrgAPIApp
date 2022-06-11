using CardOrgAPI.Application.SearchSort.Save;
using CardOrgAPI.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Controllers.Public
{
    [ApiController]
    [Route("api/public/search_sort")]
    public class SearchSortController : Controller
    {
        private readonly IMediator _mediator;

        public SearchSortController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("save"), HttpPost]
        public async Task<ActionResult<ApiResponse>> SaveSearchSortAsync([FromBody] SaveSearchSortRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(result);
        }

    }
}
