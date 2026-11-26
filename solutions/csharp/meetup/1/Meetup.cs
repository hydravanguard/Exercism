
using System;
using System.Linq;

public enum Schedule
{
    First,
    Second,
    Third,
    Fourth,
    Last,
    Teenth
}

public class Meetup
{
    private readonly int month;
    private readonly int year;

    public Meetup(int month, int year)
    {
        this.month = month;
        this.year = year;
    }

    public DateTime Day(DayOfWeek dayOfWeek, Schedule schedule)
    {
        int daysInMonth = DateTime.DaysInMonth(year, month);

        var matchingDays = Enumerable.Range(1, daysInMonth)
            .Select(day => new DateTime(year, month, day))
            .Where(date => date.DayOfWeek == dayOfWeek)
            .ToList();

        switch (schedule)
        {
            case Schedule.First:
                return matchingDays[0];

            case Schedule.Second:
                return matchingDays[1];

            case Schedule.Third:
                return matchingDays[2];

            case Schedule.Fourth:
                return matchingDays[3];

            case Schedule.Last:
                return matchingDays[matchingDays.Count - 1];

            case Schedule.Teenth:
                return matchingDays.First(date => date.Day >= 13 && date.Day <= 19);

            default:
                throw new ArgumentException("Невідомий розклад");
        }
    }
}
