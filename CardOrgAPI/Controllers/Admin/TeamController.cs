using CardOrgAPI.Application.Teams.Delete;
using CardOrgAPI.Application.Teams.Save;
using CardOrgAPI.Application.Teams.Search;
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
    public class TeamController : Controller
    {
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("teams"), HttpPost]
        public async Task<ActionResult<ApiResponse<SearchTeamsResponse>>> GetAsync([FromBody] SearchTeamsRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("teams/save"), HttpPost]
        public async Task<ActionResult<ApiResponse>> SaveAsync([FromBody] SaveTeamRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("teams/delete"), HttpPost]
        public async Task<ActionResult<ApiResponse>> DeleteYearAsync([FromBody] DeleteTeamRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }
    }
}
