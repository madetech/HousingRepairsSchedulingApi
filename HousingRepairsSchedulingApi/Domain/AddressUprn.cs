namespace HousingRepairsSchedulingApi.Domain;

using Ardalis.GuardClauses;

public class AddressUprn
{
    private readonly string addressUprn;

    private AddressUprn(string addressUprn) => this.addressUprn = addressUprn;

    public static AddressUprn Parse(string addressUprn)
    {
        Guard.Against.NullOrWhiteSpace(addressUprn, "Address UPRN");

        return new AddressUprn(addressUprn);
    }

    public override string ToString() => this.addressUprn;
}
