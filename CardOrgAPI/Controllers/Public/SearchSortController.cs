using CardOrgAPI.Application.SearchSort.Delete;
using CardOrgAPI.Application.SearchSort.Save;
using CardOrgAPI.Application.SearchSort.Search;
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

        [Route("search"), HttpPost]
        public async Task<ActionResult<ApiResponse<SearchSearchSortResponse>>> SearchSearchSortAsync([FromBody] SearchSearchSortRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(result);
        }

        [Route("delete"), HttpPost]
        public async Task<ActionResult<ApiResponse>> DeleteSearchSortAsync([FromBody] DeleteSearchSortRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(result);
        }
    }
}
