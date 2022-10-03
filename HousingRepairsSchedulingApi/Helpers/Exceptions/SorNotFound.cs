namespace HousingRepairsSchedulingApi.Helpers.Exceptions;

using System;

public class SorNotFound : Exception
{
    public SorNotFound(string sorCode) : base($"{sorCode} not found") { }
}
