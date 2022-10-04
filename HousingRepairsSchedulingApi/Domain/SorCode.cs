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

    public override string ToString() => this.sorCode;
}
