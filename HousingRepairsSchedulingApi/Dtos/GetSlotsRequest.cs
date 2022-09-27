namespace HousingRepairsSchedulingApi.Dtos;

using System;

public class GetSlotsRequest
{
    // TODO: Remove hardcoding of values
    public GetSlotsRequest(JobCodes jobCodes, DateTime appointmentDateTime, string addressUprn)
    {
        this.ClientSystemUser = "Made Tech";
        this.OperationMode = 0;
        this.ContractType = "Response";
        this.ClientContractReference = null;
        this.TradesArray = jobCodes.TradeCode;
        this.Priority = jobCodes.PriorityCode;
        this.AppointmentDateTime = appointmentDateTime;
        this.DaysAroundReturnedDate = 14;
        this.SlotDuration = 0;
        this.CapacityWeightingOption = "CapacityOptionNormal";
        this.AddressCode = addressUprn;
        this.IncludeCalendarCentralisationDetails = true;
        this.IncludeTradeAreaDetails = true;
    }

    public string ClientSystemUser { get; set; }
    public int OperationMode { get; set; }
    public string ContractType { get; set; }
    public string ClientContractReference { get; set; }
    public string TradesArray { get; set; }
    public string Priority { get; set; }
    public DateTime AppointmentDateTime { get; set; }
    public int DaysAroundReturnedDate { get; set; }
    public int SlotDuration { get; set; }
    public string CapacityWeightingOption { get; set; }
    public string AddressCode { get; set; }
    public bool IncludeCalendarCentralisationDetails { get; set; }
    public bool IncludeTradeAreaDetails { get; set; }
}
