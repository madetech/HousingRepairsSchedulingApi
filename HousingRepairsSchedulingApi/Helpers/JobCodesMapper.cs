namespace HousingRepairsSchedulingApi.Helpers;

using System;
using System.Text.Json;
using Domain;
using Exceptions;

public class JobCodesMapper : IJobCodesMapper
{
    private readonly JsonDocument jobCodes;

    public JobCodesMapper(JsonDocument jobCodes) => this.jobCodes = jobCodes;

    public JobCodes FromSorCode(string sorCode)
    {
        string tradeCode;

        try
        {
            tradeCode = this.jobCodes.RootElement.GetProperty(sorCode).GetString();
        }
        catch (Exception e)
        {
            throw new SorNotFound(sorCode);
        }

        return new JobCodes(sorCode, tradeCode);
    }
}
