using CardOrgAPI.Application.Years.Add;
using CardOrgAPI.Application.Years.Load;
using CardOrgAPI.Application.Years.Search;
using CardOrgAPI.Contexts;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using CardOrgAPI.Requests;
using CardOrgAPI.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Controllers.Admin
{
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/admin")]
    public class YearController : Controller
    {
        private readonly IMediator _mediator;

        public YearController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("years"), HttpPost]
        public async Task<ActionResult<ApiResponse<SearchYearsResponse>>> GetYearsAsync([FromBody] SearchYearsRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("year/add"), HttpPost]
        public async Task<ActionResult<ApiResponse>> AddYearsAsync([FromBody] AddYearRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("year/load"), HttpPost]
        public async Task<ActionResult<ApiResponse<YearResponse>>> LoadYearAsync([FromBody] LoadYearRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }
    }
}
