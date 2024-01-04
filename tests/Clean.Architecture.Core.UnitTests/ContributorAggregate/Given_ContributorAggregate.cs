using Clean.Architecture.Core.ContributorAggregate;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.Given_ContributorAggregate;

public class Given_ContributorAggregate
{
  private readonly string _testName = "test name";
  private Contributor? _testContributor;

  private Contributor CreateContributor()
  {
    return new Contributor(_testName);
  }

  [Fact]
  public void When_ValidName_Then_InitializeSuccessfully()
  {
    _testContributor = CreateContributor();

    Assert.Equal(_testName, _testContributor.Name);
  }
}
