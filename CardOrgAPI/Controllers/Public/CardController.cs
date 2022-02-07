﻿using CardOrgAPI.Application.Cards.Carousel;
using CardOrgAPI.Application.GradeCompanies.Delete;
using CardOrgAPI.Application.GradeCompanies.Save;
using CardOrgAPI.Application.GradeCompanies.Search;
using CardOrgAPI.Application.Years.Delete;
using CardOrgAPI.Application.Years.Save;
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

namespace CardOrgAPI.Controllers.Public
{
    //[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/public")]
    public class CardController : Controller
    {
        private readonly IMediator _mediator;

        public CardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("card_carousel"), HttpPost]
        public async Task<ActionResult<ApiResponse<IEnumerable<CardResponse>>>> GetAsync([FromBody] CardCarouselRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }
    }
}