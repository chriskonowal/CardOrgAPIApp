using CardOrgAPI.Contexts;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Models.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CardOrgAPI.Controllers.Admin
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/admin")]
    public class YearController : Controller
    {
        private readonly IYearRepository _yearRepository;

        public YearController(IYearRepository yearRepository)
        {
            _yearRepository = yearRepository;
        }

        [Route("years"), HttpGet]
        public async Task<ActionResult<IEnumerable<Year>>> GetYearsAsync(CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var Year = await _yearRepository.GetYearsAsync(cancellationToken).ConfigureAwait(false);

            return Ok(Year);
        }
    }
}
