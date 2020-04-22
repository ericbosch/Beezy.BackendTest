using System.Collections.Generic;
using System.Threading.Tasks;
using Beezy.BackendTest.Api.Models.Recommendations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Beezy.BackendTest.Api.Controllers.Viewers
{
    /// <inheritdoc />
    [Route("api/viewers/recommendations/movies")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        /// <summary>
        /// Get recommendations for movies filtered by keywords or genres.
        /// </summary>
        /// <remarks>
        /// All-time recommended movies based on keywords that you like, genres you prefer or a combination of both.
        /// </remarks>
        /// <param name="keywords">Keywords used to identify the movie</param>
        /// <param name="genres">Genres which movie belongs</param>
        [HttpGet("all-time")]
        [ProducesResponseType(typeof(List<MovieRecommendationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTimeRecommendations([FromQuery] List<string> keywords, [FromQuery] List<string> genres)
        {
            return NotFound("Under construction, come back soon!");
        }

        /// <summary>
        /// Get recommendations for upcoming movies filtered by keywords or genres.
        /// </summary>
        /// <remarks>
        /// Recommended upcoming movies (specifying a period of time from now) based on keywords that they like, genres they prefer or a combination of both.
        /// </remarks>
        /// <param name="timePeriod">Period of time (in days) from now</param>
        /// <param name="keywords">Keywords used to identify the movie</param>
        /// <param name="genres">Genres which movie belongs</param>
        [HttpGet("upcoming")]
        [ProducesResponseType(typeof(List<MovieRecommendationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUpcomingRecommendations([FromQuery, BindRequired] int timePeriod, [FromQuery] List<string> keywords, [FromQuery] List<string> genres)
        {
            return NotFound("Under construction, come back soon!");
        }
    }
}
