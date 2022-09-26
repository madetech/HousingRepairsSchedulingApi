namespace HousingRepairsSchedulingApi.Factories;
using Dtos;

public class JobIdentifierFactory
{
    public JobIdentifier FromSorCode(string sorCode)
    {
        // TODO : actual implementation
        return new JobIdentifier() { SorCode = sorCode, TradeCode = "foo", PriorityCode = "bar" };
    }
}
