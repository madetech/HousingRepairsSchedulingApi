namespace HousingRepairsSchedulingApi.Dtos;

// TODO: Think of a better name for this
public class JobIdentifier
{
    public string SorCode { get; set; }
    public string TradeCode { get; set; }
    public string PriorityCode { get; set; }

    // TODO : TO ADD OTHER OTHER IDENTIFIERS DERIVED FROM SORCODE
}
