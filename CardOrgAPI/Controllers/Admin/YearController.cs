using CardOrgAPI.Application.Years;
using CardOrgAPI.Contexts;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
using CardOrgAPI.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        //[Route("years/{page:int}"), HttpGet]
        //public async Task<ActionResult<IEnumerable<Year>>> GetYearsAsync(int page, CancellationToken cancellationToken)
        //{

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var Year = await _yearRepository.GetYearsAsync(page, cancellationToken).ConfigureAwait(false);

        //    return Ok(Year);
        //}

        [Route("years/search/{page:int}"), HttpPost]
        public async Task<ActionResult<IEnumerable<Year>>> SearchYearsAsync(int page, [FromBody] YearSearchRequest yearSearchRequest, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var request = new SearchYearsRequest() {
                Page = page,
                SearchYear = yearSearchRequest.Year
            };

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }
    }
}
