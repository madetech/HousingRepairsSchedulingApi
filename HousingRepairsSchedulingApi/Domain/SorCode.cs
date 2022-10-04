namespace HousingRepairsSchedulingApi.Domain;

using Ardalis.GuardClauses;

public class SorCode
{
    private readonly string sorCode;

    private SorCode(string sorCode) => this.sorCode = sorCode;

    public static SorCode Parse(string sorCode)
    {
        Guard.Against.NullOrWhiteSpace(sorCode, "SoR Code");

        return new SorCode(sorCode);
    }

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

        return obj.GetType() == this.GetType() && this.Equals((SorCode)obj);
    }

    private bool Equals(SorCode other) => this.sorCode == other.sorCode;

    public override int GetHashCode() => this.sorCode != null ? this.sorCode.GetHashCode() : 0;

    public override string ToString() => this.sorCode;
}
