namespace PharmacyOnline.Services.Statistics
{
    public interface IStatistics
    {
        Task<int> getSumitedMonth();
        Task<int> getCheckedMonth();
        Task<int> getQualifiedMonth();
        Task<int> getUnQualifiedMonth();

        Task<int> getSumitedWeek();
        Task<int> getCheckedWeek();
        Task<int> getQualifiedWeek();
        Task<int> getUnQualifiedWeek();


        Task<int> getSumitedDay();
        Task<int> getCheckedDay();
        Task<int> getQualifiedDay();
        Task<int> getUnQualifiedDay();

    }
}
