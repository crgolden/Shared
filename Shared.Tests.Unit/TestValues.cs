namespace Shared.Tests.Unit;

using System.Globalization;

internal static class TestValues
{
    internal static string LowercaseToken(int length) =>
        string.Concat(Enumerable.Range(0, length).Select(_ => (char)Random.Shared.Next('a', 'z' + 1)));

    internal static string NewName() => $"{LowercaseToken(6)} {LowercaseToken(8)}";

    internal static string NewSlug() => $"{LowercaseToken(6)}-{LowercaseToken(8)}";

    internal static string NewCity() => LowercaseToken(9);

    internal static string NewStateCode() =>
        $"{(char)Random.Shared.Next('A', 'Z' + 1)}{(char)Random.Shared.Next('A', 'Z' + 1)}";

    internal static string NewZip() => Random.Shared.Next(10000, 100000).ToString(CultureInfo.InvariantCulture);

    internal static string NewStreet() => $"{Random.Shared.Next(100, 10000)} {LowercaseToken(10)} street";

    internal static string NewLanguage() => LowercaseToken(7);

    internal static string NewDescription() => $"{LowercaseToken(5)} {LowercaseToken(9)}";

    internal static double NewLatitude() => Math.Round((Random.Shared.NextDouble() * 180.0) - 90.0, 6);

    internal static double NewLongitude() => Math.Round((Random.Shared.NextDouble() * 360.0) - 180.0, 6);

    internal static int NewWorshipStyleCode() => Random.Shared.Next(0, 6);

    internal static byte NewDayOfWeek() => (byte)Random.Shared.Next(0, 7);

    internal static TimeOnly NewTimeOfDay() => new TimeOnly(Random.Shared.Next(0, 24), Random.Shared.Next(0, 60));

    internal static decimal NewConfidenceScore() => Math.Round((decimal)Random.Shared.NextDouble(), 4);

    internal static DateTimeOffset NewUtcTimestamp() =>
        DateTimeOffset.UtcNow.AddMinutes(-Random.Shared.Next(1, 100000));

    internal static DateTimeOffset NewTimestampWithNonZeroOffset() =>
        NewUtcTimestamp().ToOffset(TimeSpan.FromHours(-Random.Shared.Next(1, 13)));
}
