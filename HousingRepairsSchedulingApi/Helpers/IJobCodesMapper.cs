namespace HousingRepairsSchedulingApi.Helpers;

using Domain;

public interface IJobCodesMapper
{
    public JobCodes FromSorCode(string sorCode);
}
