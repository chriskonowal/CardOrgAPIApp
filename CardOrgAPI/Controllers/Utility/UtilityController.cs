using CardOrgAPI.Application.GradeCompanies.Search;
using CardOrgAPI.Application.Utility.CreateCSVFiles;
using CardOrgAPI.Application.Utility.DeletePictures;
using CardOrgAPI.Application.Utility.IPhone7Cards;
using CardOrgAPI.Application.Utility.PopulateTables;
using CardOrgAPI.Application.Utility.ResizePictures;
using CardOrgAPI.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Controllers.Utility
{
    [Route("api/utility")]
    public class UtilityController : Controller
    {
        private readonly IMediator _mediator;

        public UtilityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("resize_pictures"), HttpGet]
        public async Task<ActionResult<ApiResponse<ResizePicturesResponse>>> ResizeAsync(CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(new ResizePicturesRequest(), cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("delete_pictures"), HttpGet]
        public async Task<ActionResult<ApiResponse<int>>> DeleteAsync(CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(new DeletePicturesRequest(), cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("save/create_csvs"), HttpGet]
        public async Task<ActionResult<ApiResponse<CreateCSVFilesResponse>>> CreateCSVsAsync(CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(new CreateCSVFilesRequest(), cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("populate_tables"), HttpGet]
        public async Task<ActionResult<ApiResponse<PopulateTablesResponse>>> PopulateTablesAsync(CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(new PopulateTablesRequest(), cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }

        [Route("reports/image_information"), HttpGet]
        public async Task<ActionResult<ApiResponse<IPhone7CardsResponse>>> GetIPhoneInformationAsync(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var years = await _mediator.Send(new IPhone7CardsRequest(), cancellationToken).ConfigureAwait(false);

            return Ok(years);
        }
    }
}
