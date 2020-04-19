using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beezy.BackendTest.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Beezy.BackendTest.Api.Controllers.Managers
{
    /// <inheritdoc />
    [Route("api/managers/recommendations/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        /// <summary>
        /// Get recommendations for upcoming movies filtered by age rating and genres.
        /// </summary>
        /// <remarks>
        /// Recommended upcoming movies (specifying a period of time from now) based on age rating and genres they prefer.
        /// </remarks>
        /// <param name="timePeriod">Period of time (in days) from now</param>
        /// <param name="ageRate">Age rating qualification of the movie</param>
        /// <param name="genres">Genres which movie belongs</param>
        [HttpGet("upcoming")]
        [ProducesResponseType(typeof(MoviesRecommendationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUpcomingRecommendations([FromQuery, BindRequired] int timePeriod,
            [FromQuery, BindRequired] List<string> ageRate, [FromQuery, BindRequired] List<string> genres)
        {
            return NotFound("Under construction, come back soon!");
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
