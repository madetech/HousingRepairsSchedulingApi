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

    private bool Equals(AddressUprn other) => this.addressUprn == other.addressUprn;

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == this.GetType() && this.Equals((AddressUprn)obj);
    }

    public override int GetHashCode() => this.addressUprn != null ? this.addressUprn.GetHashCode() : 0;
}
