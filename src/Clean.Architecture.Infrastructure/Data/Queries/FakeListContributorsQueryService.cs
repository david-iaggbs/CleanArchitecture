using Clean.Architecture.UseCases.Contributors;
using Clean.Architecture.UseCases.Contributors.List;

namespace Clean.Architecture.Infrastructure.Data.Queries;

public class FakeListContributorsQueryService : IListContributorsQueryService
{
  public Task<IEnumerable<ContributorDTO>> ListAsync()
  {
    // List<ContributorDTO> result = 
    //     [new ContributorDTO(1, "Fake Contributor 1", ""), 
    //     new ContributorDTO(2, "Fake Contributor 2", "")];
    List<ContributorDTO> result = 
        [new ContributorDTO(1, SeedData.Contributor1.Name, ""), 
        new ContributorDTO(2, SeedData.Contributor2.Name, "")];

    return Task.FromResult(result.AsEnumerable());
  }
}
