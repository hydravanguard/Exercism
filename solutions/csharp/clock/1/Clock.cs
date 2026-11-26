
using System;

public class Clock : IEquatable<Clock>
{
    private readonly int totalMinutes;

    public Clock(int hours, int minutes)
    {
        int total = hours * 60 + minutes;
        totalMinutes = ((total % 1440) + 1440) % 1440;
    }

    public Clock Add(int minutes)
    {
        return new Clock(0, totalMinutes + minutes);
    }

    public Clock Subtract(int minutes)
    {
        return new Clock(0, totalMinutes - minutes);
    }

    public override string ToString()
    {
        int hours = totalMinutes / 60;
        int minutes = totalMinutes % 60;

        return $"{hours:D2}:{minutes:D2}";
    }

    public bool Equals(Clock other)
    {
        return other != null && totalMinutes == other.totalMinutes;
    }

    public override bool Equals(object obj)
    {
        return obj is Clock other && Equals(other);
    }

    public override int GetHashCode()
    {
        return totalMinutes.GetHashCode();
    }

    public static bool operator ==(Clock left, Clock right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Clock left, Clock right)
    {
        return !(left == right);
    }
}
