using Ardalis.HttpClientTestExtensions;
using Clean.Architecture.Infrastructure.Data;
using Clean.Architecture.Web.Endpoints.ContributorEndpoints;
using Xunit;

namespace Clean.Architecture.Web.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class Given_ContributorList(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client = factory.CreateClient();

  [Fact]
  public async Task When_TwoContributors_Then_ReturnsTwoContributors()
  {
    var result = await _client.GetAndDeserializeAsync<ContributorListResponse>("/Contributors");

    Assert.Equal(2, result.Contributors.Count);
    Assert.Contains(result.Contributors, i => i.Name == SeedData.Contributor1.Name);
    Assert.Contains(result.Contributors, i => i.Name == SeedData.Contributor2.Name);
  }
}
