using System.Collections.Generic;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard.Models;
using Optional;

namespace Beezy.BackendTest.Domain.Queries.IntelligentBillboard
{
    public class GetIntelligentBillboardResponse
    {
        public Option<List<Billboard>> Billboards { get; }

        public GetIntelligentBillboardResponse(Option<List<Billboard>> billboards)
        {
            Billboards = billboards;
        }
    }
}
