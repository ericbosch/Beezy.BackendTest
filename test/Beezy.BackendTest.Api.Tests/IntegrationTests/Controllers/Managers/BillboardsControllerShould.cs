using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Beezy.BackendTest.Api.Models.Billboards;
using Beezy.BackendTest.Domain.Queries.IntelligentBillboard;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ServiceStack;
using Xunit;

namespace Beezy.BackendTest.Api.Tests.IntegrationTests.Controllers.Managers
{
    public class BillboardsControllerShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public BillboardsControllerShould(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task ReturnMoviesFromProxy()
        {
            var billboardRequest = BogusData.BogusIntelligentBillboardData.GetIntelligentBillboardRequestsList(5);
            var firstRequestExpected = billboardRequest.First();

            var request = new GetIntelligentBillboardRequest(
                firstRequestExpected.TimePeriod, 
                firstRequestExpected.BigRooms, 
                firstRequestExpected.SmallRooms, 
                false);

            var response = await _client.GetAsync($"/api/managers/billboards/intelligent{ BuildQueryParams(request) }");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var result =
                JsonConvert.DeserializeObject<List<IntelligentBillboardResponse>>(jsonResponse);

            result.Should().HaveCount(request.TimePeriod);
            result.Should().OnlyHaveUniqueItems();
            result.Should().Contain(b => b.SmallScreenMovies.Count == request.SmallRooms);
            result.Should().Contain(b => b.BigScreenMovies.Count == request.BigRooms);
            result.Select(r => r.StartDate).Should().BeInAscendingOrder();
        }

        private string BuildQueryParams(GetIntelligentBillboardRequest request)
        {
            return string.Empty
                .AddQueryParam("timePeriod", request.TimePeriod)
                .AddQueryParam("bigRooms", request.BigRooms)
                .AddQueryParam("smallRooms", request.SmallRooms)
                .AddQueryParam("basedOnCity", request.BasedOnCity)
                .AddQueryParam("api-version", "1.0")
                ;
        }
    }
}
