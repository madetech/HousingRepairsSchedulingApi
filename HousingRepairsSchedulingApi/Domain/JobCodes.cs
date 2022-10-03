namespace HousingRepairsSchedulingApi.Domain;

public class JobCodes
{
    public JobCodes(string sorCode, string tradeCode)
    {
        this.SorCode = sorCode;
        this.TradeCode = tradeCode;
    }

    public string SorCode { get; }
    public string TradeCode { get; }
}
