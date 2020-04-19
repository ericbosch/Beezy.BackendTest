using System.Collections.Generic;
using System.Threading.Tasks;
using Beezy.BackendTest.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Beezy.BackendTest.Api.Controllers.Viewers
{
    [Route("api/recommendations/tv-shows")]
    [ApiController]
    public class TvShowsController : ControllerBase
    {
        [HttpGet("all-time")]
        [ProducesResponseType(typeof(TvShowsRecommendationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTimeRecommendations([FromQuery] List<string> keywords, [FromQuery] List<string> genres)
        {
            return NotFound("Under construction, get back soon!");
        }
    }
}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously