using System.Collections.Generic;
using System.Threading.Tasks;
using Beezy.BackendTest.Api.Models.Recommendations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Beezy.BackendTest.Api.Controllers.Viewers
{
    /// <inheritdoc />
    [Route("api/viewers/recommendations/documentaries")]
    [ApiController]
    public class DocumentariesController : ControllerBase
    {
        /// <summary>
        /// Get recommendations for documentaries filtered by topics.
        /// </summary>
        /// <remarks>
        /// All-time recommended documentaries based on topics.
        /// </remarks>
        /// <param name="topics">Topics covered by the documentary</param>
        [HttpGet("all-time")]
        [ProducesResponseType(typeof(List<DocumentaryRecommendationResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllTimeRecommendations([FromQuery] List<string> topics)
        {
            return NotFound("Under construction, come back soon!");
        }
    }
}
