namespace HousingRepairsSchedulingApi.Factories;

using Dtos;

public class JobIdentifierFactory
{
    public JobIdentifier FromSorCode(string sorCode) =>
        // TODO: actual implementation
        new() { SorCode = sorCode, TradeCode = "EL", PriorityCode = "R" };
}
