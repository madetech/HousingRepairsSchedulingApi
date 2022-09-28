namespace HousingRepairsSchedulingApi.Gateways.Exceptions;

using System;

public class McmRequestError : Exception
{
    public McmRequestError(string errorCode, string errorMessage) : base(
        $"StatusCode: {errorCode}\nStatusMessage: {errorMessage}")
    {
    }
}
