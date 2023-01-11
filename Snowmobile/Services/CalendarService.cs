using SnowmobileShop.Data;
using SnowmobileShop.Models;

namespace SnowmobileShop.Services
{
    public interface ICalendarService
    {
        IEnumerable<RentalTime> GenerateRentalHours(int hours, RentalDay day);
    }

    public class CalendarService : ICalendarService
    {
        private readonly ApplicationDbContext _dbContext;

        public CalendarService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IEnumerable<RentalTime> GenerateRentalHours(int hours, RentalDay day)
        {
            var rentalHours = new List<RentalTime>();

            // 10:00 - 22:00

            var start = new TimeOnly(10, 0, 0);
            var end = start.AddHours(hours);
            int wrappedDays = 0;

            while (true)
            {
                if (end.Hour > 22 || wrappedDays > 0)
                    break;

                var rentalTime = new RentalTime
                {
                    From = start,
                    To = end,
                    RentalDay = day,
                    IsReserved = false,
                };

                rentalHours.Add(rentalTime);

                _dbContext.RentalHours.Add(rentalTime);
                _dbContext.SaveChanges();

                start = start.AddHours(hours);
                end = end.AddHours(hours, out wrappedDays);
            }

            return rentalHours;
        }
    }
}
