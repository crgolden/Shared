namespace Shared.Tests.Unit;

using Shared.Domain;
using static Testing.Generated;

internal static class TestValues
{
    internal static StateCode NewStateCode()
    {
        var defined = Enum.GetValues<StateCode>();
        return defined[Random.Shared.Next(defined.Length)];
    }

    internal static StateCode NewUndefinedStateCode() =>
        (StateCode)(Enum.GetValues<StateCode>().Max(code => (int)code) + Random.Shared.Next(1, 100));

    internal static string NewNonUspsLetterPair()
    {
        var defined = Enum.GetNames<StateCode>();
        string pair;
        do
        {
            pair = $"{(char)Random.Shared.Next('A', 'Z' + 1)}{(char)Random.Shared.Next('A', 'Z' + 1)}";
        }
        while (defined.Contains(pair, StringComparer.OrdinalIgnoreCase));

        return pair;
    }

    internal static string NewAttributeKey() => LowercaseToken(11);

    internal static string NewAttributeSource() => LowercaseToken(10);

    internal static int NewWorshipStyleCode() => Random.Shared.Next(0, 6);

    internal static decimal NewOutOfRangeOffset() => Random.Shared.Next(1, 1001) / 10000m;
}
