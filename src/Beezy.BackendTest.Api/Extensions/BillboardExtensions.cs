using System.Linq;
using Beezy.BackendTest.Api.Models.Billboards;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
#pragma warning disable 1591

namespace Beezy.BackendTest.Api.Extensions
{
    public static class BillboardExtensions
    {
        public static IntelligentBillboardResponse ToDto(this Billboard billboard)
        {
            return new IntelligentBillboardResponse()
            {
                StartDate = billboard.StartDate,
                BigScreenMovies = billboard.BigScreenMovies.ToDto().ToList(),
                SmallScreenMovies = billboard.SmallScreenMovies.ToDto().ToList()
            };
        }
    }
}
