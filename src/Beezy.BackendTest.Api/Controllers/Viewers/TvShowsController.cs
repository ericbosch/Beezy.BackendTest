using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Beezy.BackendTest.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Beezy.BackendTest.Api.Controllers.Viewers
{
    /// <inheritdoc />
    [Route("api/viewers/recommendations/tv-shows")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        /// <summary>
        /// Get recommendations for TV shows filtered by keywords or genres.
        /// </summary>
        /// <remarks>
        /// All-time recommended TV shows based on keywords that you like, genres you prefer or a combination of both.
        /// </remarks>
        /// <param name="keywords">Keywords used to identify the TV show</param>
        /// <param name="genres">Genres which TV show belongs</param>
        [HttpGet("all-time")]
        [ProducesResponseType(typeof(TvShowsRecommendationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTimeRecommendations([FromQuery] List<string> keywords, [FromQuery] List<string> genres)
        {
            return NotFound("Under construction, come back soon!");
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously