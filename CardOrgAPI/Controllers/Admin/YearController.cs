using CardOrgAPI.Contexts;
using CardOrgAPI.Interfaces.Repositories;
using CardOrgAPI.Model;
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
