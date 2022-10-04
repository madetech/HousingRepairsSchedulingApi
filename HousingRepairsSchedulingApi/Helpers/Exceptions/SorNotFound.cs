namespace HousingRepairsSchedulingApi.Helpers.Exceptions;

using System;
using Domain;

public class SorNotFound : Exception
{
    public SorNotFound(SorCode sorCode) : base($"{sorCode} not found") { }
}
