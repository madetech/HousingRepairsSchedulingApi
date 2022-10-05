namespace HousingRepairsSchedulingApi.Helpers;

using System.Diagnostics.CodeAnalysis;
using Domain;

public interface IJobCodesMapper
{
    public JobCodes FromSorCode([NotNull] SorCode sorCode);
}
