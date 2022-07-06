using CardOrgAPI.Application.Sports.Delete;
using CardOrgAPI.Application.Sports.Save;
using CardOrgAPI.Application.Sports.Search;
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
    public class SportController : Controller
    {
        private readonly IMediator _mediator;

        public SportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("sports"), HttpPost]
        public async Task<ActionResult<ApiResponse<SearchSportsResponse>>> GetAsync([FromBody] SearchSportsRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("sports/save"), HttpPost]
        public async Task<ActionResult<ApiResponse>> SaveAsync([FromBody] SaveSportRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("sports/delete"), HttpPost]
        public async Task<ActionResult<ApiResponse>> DeleteYearAsync([FromBody] DeleteSportRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }
    }
}
