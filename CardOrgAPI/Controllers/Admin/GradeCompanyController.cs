using CardOrgAPI.Application.GradeCompanies.Delete;
using CardOrgAPI.Application.GradeCompanies.Save;
using CardOrgAPI.Application.GradeCompanies.Search;
using CardOrgAPI.Application.Years.Delete;
using CardOrgAPI.Application.Years.Save;
using CardOrgAPI.Application.Years.Search;
using CardOrgAPI.Entities;
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
    public class GradeCompanyController : Controller
    {
        private readonly IMediator _mediator;

        public GradeCompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("grade_companies"), HttpPost]
        public async Task<ActionResult<ApiResponse<SearchGradeCompaniesResponse>>> GetAsync([FromBody] SearchGradeCompaniesRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("grade_companies/save"), HttpPost]
        public async Task<ActionResult<ApiResponse>> SaveAsync([FromBody] SaveGradeCompanyRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("grade_companies/delete"), HttpPost]
        public async Task<ActionResult<ApiResponse>> DeleteYearAsync([FromBody] DeleteGradeCompanyRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }
    }
}
