using Clean.Architecture.Core.ContributorAggregate;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Clean.Architecture.Infrastructure.IntegrationTests.Data;

public class Given_EfRepository : BaseEfRepoTestFixture
{
  [Fact]
  public async Task When_AddsContributor_Then_SetsId_Successfully()
  {
    var testContributorName = "testContributor";
    var testContributorStatus = ContributorStatus.NotSet;
    var repository = GetRepository();
    var Contributor = new Contributor(testContributorName);

    await repository.AddAsync(Contributor);

    var newContributor = (await repository.ListAsync())
                    .FirstOrDefault();

    Assert.Equal(testContributorName, newContributor?.Name);
    Assert.Equal(testContributorStatus, newContributor?.Status);
    Assert.True(newContributor?.Id > 0);
  }

  [Fact]
  public async Task When_UpdatesItemAfterAddingIt_Then_AllPropertiesUpdated_Successfully()
  {
    // add a Contributor
    var repository = GetRepository();
    var initialName = Guid.NewGuid().ToString();
    var Contributor = new Contributor(initialName);

    await repository.AddAsync(Contributor);

    // detach the item so we get a different instance
    _dbContext.Entry(Contributor).State = EntityState.Detached;

    // fetch the item and update its title
    var newContributor = (await repository.ListAsync())
        .FirstOrDefault(Contributor => Contributor.Name == initialName);
    if (newContributor == null)
    {
      Assert.NotNull(newContributor);
      return;
    }
    Assert.NotSame(Contributor, newContributor);
    var newName = Guid.NewGuid().ToString();
    newContributor.UpdateName(newName);

    // Update the item
    await repository.UpdateAsync(newContributor);

    // Fetch the updated item
    var updatedItem = (await repository.ListAsync())
        .FirstOrDefault(Contributor => Contributor.Name == newName);

    Assert.NotNull(updatedItem);
    Assert.NotEqual(Contributor.Name, updatedItem?.Name);
    Assert.Equal(Contributor.Status, updatedItem?.Status);
    Assert.Equal(newContributor.Id, updatedItem?.Id);
  }

  [Fact]
  public async Task When_DeletesItemAfterAddingIt_Then_ItemDeleted_Succesfully()
  {
    // add a Contributor
    var repository = GetRepository();
    var initialName = Guid.NewGuid().ToString();
    var Contributor = new Contributor(initialName);
    await repository.AddAsync(Contributor);

    // delete the item
    await repository.DeleteAsync(Contributor);

    // verify it's no longer there
    Assert.DoesNotContain(await repository.ListAsync(),
        Contributor => Contributor.Name == initialName);
  }
}
