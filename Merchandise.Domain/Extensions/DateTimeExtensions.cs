namespace Merchandise.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly TimeZoneInfo PhilippineTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("Singapore Standard Time");

        public static DateTime ToPhilippineTime(this DateTime dateTime)
        {
            return dateTime.Kind switch
            {
                DateTimeKind.Utc => TimeZoneInfo.ConvertTimeFromUtc(dateTime, PhilippineTimeZone),
                DateTimeKind.Local => TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, PhilippineTimeZone),
                _ => TimeZoneInfo.ConvertTime(dateTime, PhilippineTimeZone)
            };
        }

        public static DateTime? ToPhilippineTime(this DateTime? dateTime)
        {
            if (dateTime == null)
                return null;

            return dateTime.Value.ToPhilippineTime();
        }
    }
}
