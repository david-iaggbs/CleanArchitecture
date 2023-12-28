using Ardalis.SharedKernel;

namespace Clean.Architecture.Core.ContributorAggregate;

public class PhoneNumber : ValueObject
{
  public string CountryCode { get; private set; } = string.Empty;
  public string Number { get; private set; } = string.Empty;
  public string? Extension { get; private set; } = string.Empty;

  public PhoneNumber(string countryCode,
    string number,
    string? extension)
  {
    CountryCode = countryCode;
    Number = number;
    Extension = extension;
  }
  protected override IEnumerable<object> GetEqualityComponents()
  {
    yield return CountryCode;
    yield return Number;
    yield return Extension ?? String.Empty;
  }
}
