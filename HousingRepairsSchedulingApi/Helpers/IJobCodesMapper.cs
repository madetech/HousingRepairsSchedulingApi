namespace HousingRepairsSchedulingApi.Helpers;

using Domain;
using JetBrains.Annotations;

public interface IJobCodesMapper
{
    public JobCodes FromSorCode([NotNull] SorCode sorCode);
}
