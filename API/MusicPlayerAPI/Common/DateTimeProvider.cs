using MusicPlayerAPI.Interfaces;

namespace MusicPlayerAPI.Common
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}
