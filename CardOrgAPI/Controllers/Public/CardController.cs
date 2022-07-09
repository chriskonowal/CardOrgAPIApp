using CardOrgAPI.Application.Cards.Carousel;
using CardOrgAPI.Application.Cards.Delete;
using CardOrgAPI.Application.Cards.LandingPage;
using CardOrgAPI.Application.Cards.RookieAutoPatchGraph;
using CardOrgAPI.Application.Cards.Save;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        public async Task<ActionResult<ApiResponse<IEnumerable<CardResponse>>>> GetCarouselAsync([FromBody] CardCarouselRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cards = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(cards);
        }

        [Route("cards"), HttpPost]
        public async Task<ActionResult<ApiResponse<LandingPageResponse>>> GetAsync([FromBody] LandingPageRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cards = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(cards);
        }

        [Route("rookie_auto_patch_graph"), HttpGet]
        public async Task<ActionResult<ApiResponse<LandingPageResponse>>> GetRookieAutoPatchGraphAsync(CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cards = await _mediator.Send(new RookieAutoPatchGraphRequest(), cancellationToken).ConfigureAwait(false);

            return Ok(cards);
        }

        [Route("cards/save"), HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult<ApiResponse>> SaveAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var files = Request.Form.Files;
                string jsonRequest = Request.Form["cardRequest"];
                CardRequest cardRequest = Newtonsoft.Json.JsonConvert.DeserializeObject<CardRequest>(jsonRequest);

                if (files != null && files.Count() == 2)
                {
                    cardRequest.FrontImage = files[0];
                    cardRequest.BackImage = files[1];
                }
                var request = new SaveCardRequest()
                {
                    CardRequest = cardRequest
                };

                var response = await _mediator.Send(request, CancellationToken.None).ConfigureAwait(false);


                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse() { 
                    ErrorMessage = ex.Message + ex.StackTrace,
                    IsSuccessful = false
                };
                return Ok(response);
            }
            

            

            //return Ok(years);
        }

        [Route("cards/delete"), HttpPost]
        public async Task<ActionResult<ApiResponse>> DeleteYearAsync([FromBody] DeleteCardRequest request, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _mediator.Send(request, cancellationToken).ConfigureAwait(false);

            return Ok(response);
        }
    }
}
