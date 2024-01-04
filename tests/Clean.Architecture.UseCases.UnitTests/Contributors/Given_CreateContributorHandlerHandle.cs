using Ardalis.SharedKernel;
using Clean.Architecture.Core.ContributorAggregate;
using Clean.Architecture.UseCases.Contributors.Create;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Clean.Architecture.UseCases.UnitTests.Contributors;

public class Given_CreateContributorHandlerHandle
{
  private readonly string _testName = "test name";
  private readonly IRepository<Contributor> _repository = Substitute.For<IRepository<Contributor>>();
  private CreateContributorHandler _handler;

  public Given_CreateContributorHandlerHandle()
  {
      _handler = new CreateContributorHandler(_repository);
  }

  private Contributor CreateContributor()
  {
    return new Contributor(_testName);
  }

  [Fact]
  public async Task When_GivenValidName_Then_ReturnsSuccess()
  {
    _repository.AddAsync(Arg.Any<Contributor>(), Arg.Any<CancellationToken>())
      .Returns(Task.FromResult(CreateContributor()));
    var result = await _handler.Handle(new CreateContributorCommand(_testName, null), CancellationToken.None);

    result.IsSuccess.Should().BeTrue();    
  }
}
