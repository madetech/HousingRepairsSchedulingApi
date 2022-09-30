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
        this.SlotDuration = 2;
        this.CapacityWeightingOption = "CapacityOptionNormal";
        this.AddressCode = addressUprn;
        this.IncludeCalendarCentralisationDetails = true;
        this.IncludeTradeAreaDetails = true;
    }

    public string ClientSystemUser { get; }
    public int OperationMode { get; }
    public string ContractType { get; }
    public string ClientContractReference { get; }
    public string TradesArray { get; }
    public string Priority { get; }
    public DateTime AppointmentDateTime { get; }
    public int DaysAroundReturnedDate { get; }
    public int SlotDuration { get; }
    public string CapacityWeightingOption { get; }
    public string AddressCode { get; }
    public bool IncludeCalendarCentralisationDetails { get; }
    public bool IncludeTradeAreaDetails { get; }
}
