namespace HousingRepairsSchedulingApi.Domain;

public class JobCodes
{
    public JobCodes(SorCode sorCode, string tradeCode)
    {
        this.SorCode = sorCode;
        this.TradeCode = tradeCode;
    }

    public SorCode SorCode { get; }
    public string TradeCode { get; }
}
