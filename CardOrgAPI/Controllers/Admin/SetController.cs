using CardOrgAPI.Application.Sets.Search;
using CardOrgAPI.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Controllers.Admin
{
    [ApiController]
    [Route("api/admin")]
    public class SetController : Controller
    {
        private readonly IMediator _mediator;

        public SetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("sets"), HttpPost]
        public async Task<ActionResult<ApiResponse<SearchSetsResponse>>> GetAsync([FromBody] SearchSetsRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }
    }
}
