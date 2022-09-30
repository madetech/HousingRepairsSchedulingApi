namespace HousingRepairsSchedulingApi.Factories;

using Dtos;

public class JobCodesFactory
{
    public JobCodes FromSorCode(string sorCode) =>
        // TODO: actual implementation
        new() { SorCode = sorCode, TradeCode = "EL" };
}