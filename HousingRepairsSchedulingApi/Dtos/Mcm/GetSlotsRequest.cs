namespace HousingRepairsSchedulingApi.Dtos.Mcm;

using System;
using Domain;

public record GetSlotsRequest
{
    // TODO: Remove hardcoding of values
    public GetSlotsRequest(JobCodes jobCodes, DateTime appointmentDateTime, AddressUprn addressUprn)
    {
        this.ClientSystemUser = "Made Tech";
        this.OperationMode = 0;
        this.ContractType = "Response";
        this.ClientContractReference = null;
        this.TradesArray = jobCodes.TradeCode;
        this.Priority = "R";
        this.AppointmentDateTime = appointmentDateTime;
        this.DaysAroundReturnedDate = 14;
        this.SlotDuration = 2;
        this.CapacityWeightingOption = "CapacityOptionNormal";
        this.AddressCode = addressUprn.ToString();
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
