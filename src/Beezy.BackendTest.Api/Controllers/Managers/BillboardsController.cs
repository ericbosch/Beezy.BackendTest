using System.Collections.Generic;
using System.Threading.Tasks;
using Beezy.BackendTest.Api.Models.Billboards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

namespace Beezy.BackendTest.Api.Controllers.Managers
{
    /// <inheritdoc />
    [Route("api/managers/billboards")]
    [ApiController]
    public class BillboardsController : ControllerBase
    {
        /// <summary>
        /// Build a suggested billboard for theaters for a specific period of one or more weeks.
        /// </summary>
        /// <remarks>
        /// Build a suggested billboard for theaters for a specific period of one or more weeks specifying the number of weeks and the number of screens.
        /// </remarks>
        /// <param name="timePeriod">Period of time (in weeks) from now</param>
        /// <param name="screenNumber">Number of screens for the theater</param>
        /// <param name="basedOnCity">Include movies that have been successful in the city</param>
        [HttpGet("suggested")]
        [ProducesResponseType(typeof(List<SuggestedBillboardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSuggestedBillboard([FromQuery, BindRequired] int timePeriod, [FromQuery, BindRequired] int screenNumber, [FromQuery] bool basedOnCity)
        {
            return NotFound("Under construction, come back soon!");
        }

        /// <summary>
        /// Build a suggested intelligent billboard for theaters for a specific period of one or more weeks.
        /// </summary>
        /// <remarks>
        /// Build a suggested intelligent billboard for theaters for a specific period of one or more weeks specifying the number of weeks
        /// and how many screens are in big rooms and how many of them in small rooms.
        /// </remarks>
        /// <param name="timePeriod">Period of time (in weeks) from now</param>
        /// <param name="bigRooms">Number of big room screens for the theater</param>
        /// <param name="smallRooms">Number of small room screens for the theater</param>
        /// <param name="basedOnCity">Include movies that have been successful in the city</param>
        [HttpGet("intelligent")]
        [ProducesResponseType(typeof(List<IntelligentBillboardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIntelligentBillboard([FromQuery, BindRequired] int timePeriod,
            [FromQuery, BindRequired] int bigRooms, [FromQuery, BindRequired] int smallRooms,
            [FromQuery] bool basedOnCity)
        {
            return NotFound("Under construction, come back soon!");
        }
    }
}
