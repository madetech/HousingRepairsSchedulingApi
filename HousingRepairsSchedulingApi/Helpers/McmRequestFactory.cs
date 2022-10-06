namespace HousingRepairsSchedulingApi.Helpers;

using System;
using System.Collections.Generic;
using Domain;
using Dtos.Mcm;
using Extensions;

public class McmRequestFactory
{
    private const string CapacityWeightingOption = "CapacityOptionNormal";
    private const string ClientContractReference = null;
    private const string ClientSystemReference = "Made Tech";
    private const string ClientSystemUser = "Made Tech";
    private const string ContractReference = "MORR7";
    private const string ContractType = "Response";
    private const string ExpenditureCode = "HPPP";
    private const string Priority = "R";

    public AddJobRequest AddJobRequest(
        string bookingReference,
        JobCodes jobCodes,
        AddressUprn addressUprn,
        Contact contact,
        string jobDescription
    ) =>
        new()
        {
            AddressUPRN = addressUprn.ToString(),
            ClientSystemReference = ClientSystemReference,
            ClientSystemUser = ClientSystemUser,
            ContactDetails = contact.NotificationEmail,
            ContactMobileNumber = contact.NotificationMobileNumber,
            ContactTelephoneNumber = contact.PhoneNumber,
            ContractReference = ContractReference,
            ExpenditureCode = ExpenditureCode,
            JobDescription = jobDescription,
            JobNumber = bookingReference.Substring(0, Math.Min(bookingReference.Length, 20)),
            OnHold = false,
            PriorityCode = Priority,
            Trade = jobCodes.TradeCode,
            JobLines = new List<JobLine> { new() { ScheduleCode = jobCodes.SorCode.ToString(), Quantity = 1 } }
        };

    public BookAppointmentRequest BookAppointmentRequest(
        int jobId,
        AppointmentSlot appointmentSlot,
        string tradeCode
    ) => new()
    {
        AppointmentDateTime = appointmentSlot.StartTime,
        AppointmentNotes = "N/A",
        SlotTimeDescription = appointmentSlot.McmSlotDescription(),
        Trade = tradeCode,
        ClientSystemUser = ClientSystemUser,
        JobId = jobId
    };
}
