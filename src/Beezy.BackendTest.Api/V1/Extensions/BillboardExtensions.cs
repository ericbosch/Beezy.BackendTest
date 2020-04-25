using System.Linq;
using Beezy.BackendTest.Api.V1.Models.Billboards;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;

#pragma warning disable 1591

namespace Beezy.BackendTest.Api.V1.Extensions
{
    public static class BillboardExtensions
    {
        public static IntelligentBillboardResponse ToDto(this Billboard billboard)
        {
            return IntelligentBillboardResponse.Create(billboard.StartDate,
                billboard.BigScreenMovies.ToDto().ToList(),
                billboard.SmallScreenMovies.ToDto().ToList());
        }
    }
}
