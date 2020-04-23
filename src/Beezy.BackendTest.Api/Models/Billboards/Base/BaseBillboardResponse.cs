using System;

namespace Beezy.BackendTest.Api.Models.Billboards.Base
{
    /// <summary>
    /// Model Container for billboards.
    /// </summary>
    public abstract class BaseBillboardResponse
    {
        /// <summary>
        /// Start date of the billboard to distinguish between them.
        /// </summary>
        public DateTime StartDate { get; protected set; }
    }
}
