namespace HousingRepairsSchedulingApi.Dtos.Mcm;

using System.Collections.Generic;

public record AddJobRequest
{
    public string ClientSystemUser { get; init; }
    public string ClientSystemReference { get; init; }
    public string ContractReference { get; init; }
    public string JobNumber { get; init; }
    public string AddressUPRN { get; init; }
    public string PriorityCode { get; init; }
    public string Trade { get; init; }
    public string ExpenditureCode { get; init; }
    public string JobDescription { get; init; }
    public bool OnHold { get; init; }
    public string ContactDetails { get; init; }
    public string ContactName { get; init; }
    public string ContactTelephoneNumber { get; init; }
    public string ContactMobileNumber { get; init; }
    public List<JobLine> JobLines { get; init; }
}

public record JobLine
{
    public string ScheduleCode { get; init; }
    public int Quantity { get; init; }
}
