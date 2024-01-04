using Ardalis.HttpClientTestExtensions;
using Clean.Architecture.Infrastructure.Data;
using Clean.Architecture.Web.ContributorEndpoints;
using Clean.Architecture.Web.Endpoints.ContributorEndpoints;
using Xunit;

namespace Clean.Architecture.Web.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class Given_ContributorGetById(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client = factory.CreateClient();

  [Fact]
  public async Task When_Id1_Then_ReturnsSeedContributor()
  {
    var result = await _client.GetAndDeserializeAsync<ContributorRecord>(GetContributorByIdRequest.BuildRoute(1));

    Assert.Equal(1, result.Id);
    Assert.Equal(SeedData.Contributor1.Name, result.Name);
  }

  [Fact]
  public async Task When_Id1000_Then_ReturnsNotFound()
  {
    string route = GetContributorByIdRequest.BuildRoute(1000);
    _ = await _client.GetAndEnsureNotFoundAsync(route);
  }
}
