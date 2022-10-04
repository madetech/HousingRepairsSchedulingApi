namespace HousingRepairsSchedulingApi.Dtos;

using System.Collections.Generic;

public class AddJobRequest
{
    public string ClientSystemUser { get; set; }
    public string ClientSystemReference { get; set; }
    public string ContractReference { get; set; }
    public string JobNumber { get; set; }
    public string AddressUPRN { get; set; }
    public string PriorityCode { get; set; }
    public string Trade { get; set; }
    public string ExpenditureCode { get; set; }
    public string JobDescription { get; set; }
    public bool OnHold { get; set; }
    public string ContactDetails { get; set; }
    public string ContactName { get; set; }
    public string ContactTelephoneNumber { get; set; }
    public string ContactMobileNumber { get; set; }
    public List<JobLine> JobLines { get; set; }
}

public class JobLine
{
    public string ScheduleCode { get; set; }
    public int Quantity { get; set; }
}
