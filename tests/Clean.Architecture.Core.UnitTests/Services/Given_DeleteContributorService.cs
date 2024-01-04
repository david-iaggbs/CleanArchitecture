using Ardalis.SharedKernel;
using Clean.Architecture.Core.ContributorAggregate;
using Clean.Architecture.Core.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Clean.Architecture.UnitTests.Core.Services;

public class Given_DeleteContributorService
{
  private readonly IRepository<Contributor> _repository = Substitute.For<IRepository<Contributor>>();
  private readonly IMediator _mediator = Substitute.For<IMediator>();
  private readonly ILogger<DeleteContributorService> _logger = Substitute.For<ILogger<DeleteContributorService>>();

  private readonly DeleteContributorService _service;

  public Given_DeleteContributorService()
  {
    _service = new DeleteContributorService(_repository, _mediator, _logger);
  }

  [Fact]
  public async Task When_CantFindContributor_Then_ReturnsNotFound()
  {
    var result = await _service.DeleteContributor(0);

    Assert.Equal(Ardalis.Result.ResultStatus.NotFound, result.Status);
  }
}
