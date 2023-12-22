using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmacyOnline.Common;
using PharmacyOnline.Entities;

namespace PharmacyOnline.Services.Statistics
{
    public class StatisticsRepoClass : IStatistics
    {
        private readonly PharmacyOnlineContext _context;

        public StatisticsRepoClass(PharmacyOnlineContext context)
        {
            _context = context;
        }

        public async Task<int> getCheckedDay()
        {
            try
            {
                string year = HandleLogic.getDMY().year;
                string month = HandleLogic.getDMY().month;
                string day = HandleLogic.getDMY().date;

            var parameters = new[]
            {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month),
                new SqlParameter("@day", day)
            };

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'CHECKED' AND DAY(createdAt) = @day AND MONTH(createdAt) = @month AND YEAR(createdAt) = @year", parameters).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getCheckedMonth()
        {
            try
            {
                string year = HandleLogic.getDMY().year;
                string month = HandleLogic.getDMY().month;
                var parameters = new[]
                    {
                        new SqlParameter("@year", year),
                        new SqlParameter("@month", month)
                    };

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'CHECKED' AND MONTH(createdAt) = @month AND YEAR(createdAt) = @year", parameters).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getCheckedWeek()
        {
            try
            {
                DateTime time_month = DateTime.Now;
                string currentDate = time_month.ToString("yyyy/MM/dd");
                var dataParam = new SqlParameter("@currentDate", currentDate);

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'CHECKED' AND CAST( createdAt AS date ) >=DATEADD(DAY, -7, @currentDate)", dataParam).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getQualifiedDay()
        {
            try
            {
                string year = HandleLogic.getDMY().year;
                string month = HandleLogic.getDMY().month;
                string day = HandleLogic.getDMY().date;

                var parameters = new[]
                {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month),
                new SqlParameter("@day", day)
            };

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'CHECKED' AND isAccepted = 'QUALIFIED' AND DAY(createdAt) = @day AND MONTH(createdAt) = @month AND YEAR(createdAt) = @year", parameters).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getQualifiedMonth()
        {
            try
            {
                string year = HandleLogic.getDMY().year;
                string month = HandleLogic.getDMY().month;

                var parameters = new[]
                {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'CHECKED' AND isAccepted = 'QUALIFIED' AND MONTH(createdAt) = @month AND YEAR(createdAt) = @year", parameters).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getQualifiedWeek()
        {
            try
            {
                DateTime time_month = DateTime.Now;
                string currentDate = time_month.ToString("yyyy/MM/dd");
                var dataParam = new SqlParameter("@currentDate", currentDate);

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'CHECKED' AND isAccepted = 'QUALIFIED' AND CAST( createdAt AS date ) >=DATEADD(DAY, -7, @currentDate)", dataParam).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getSumitedDay()
        {
            try
            {
                string year = HandleLogic.getDMY().year;
                string month = HandleLogic.getDMY().month;
                string day = HandleLogic.getDMY().date;

                var parameters = new[]
                {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month),
                new SqlParameter("@day", day)
            };

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'SUBMITED' AND DAY(createdAt) = @day AND MONTH(createdAt) = @month AND YEAR(createdAt) = @year", parameters).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getSumitedMonth()
        {
            try
            {
                string year = HandleLogic.getDMY().year;
                string month = HandleLogic.getDMY().month;

                var parameters = new[]
                {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'SUBMITED' AND MONTH(createdAt) = @month AND YEAR(createdAt) = @year", parameters).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getSumitedWeek()
        {
            try
            {
                DateTime time_month = DateTime.Now;
                string currentDate = time_month.ToString("yyyy/MM/dd");
                var dataParam = new SqlParameter("@currentDate", currentDate);

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'SUBMITED' AND CAST( createdAt AS date ) >=DATEADD(DAY, -7, @currentDate)", dataParam).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getUnQualifiedDay()
        {
            try
            {
                string year = HandleLogic.getDMY().year;
                string month = HandleLogic.getDMY().month;
                string day = HandleLogic.getDMY().date;

                var parameters = new[]
                {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month),
                new SqlParameter("@day", day)
            };

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'CHECKED' AND isAccepted = 'UNQUALIFIED' AND DAY(createdAt) = @day AND MONTH(createdAt) = @month AND YEAR(createdAt) = @year", parameters).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getUnQualifiedMonth()
        {
            try
            {
                string year = HandleLogic.getDMY().year;
                string month = HandleLogic.getDMY().month;

                var parameters = new[]
                {
                new SqlParameter("@year", year),
                new SqlParameter("@month", month)
            };

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'CHECKED' AND isAccepted = 'UNQUALIFIED' AND MONTH(createdAt) = @month AND YEAR(createdAt) = @year", parameters).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public async Task<int> getUnQualifiedWeek()
        {
            try
            {
                DateTime time_month = DateTime.Now;
                string currentDate = time_month.ToString("yyyy/MM/dd");
                var dataParam = new SqlParameter("@currentDate", currentDate);

                var result = _context.StatisticModel.FromSqlRaw("SELECT COUNT(id) as TotalProfile FROM personalDetail where status = 'CHECKED' AND isAccepted = 'UNQUALIFIED' AND CAST( createdAt AS date ) >=DATEADD(DAY, -7, @currentDate)", dataParam).FirstOrDefault();

                if (result == null) return 0;

                return result.TotalProfile;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}

/*
        try
            {

            }catch(Exception ex)
            {
                return new Models.Candidate.result
                {
                    status = 401,
                    statusMessage = $"bad request{ex.Message}"
                };
            }
 */