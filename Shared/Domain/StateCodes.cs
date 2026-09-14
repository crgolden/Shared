namespace Shared.Domain;

public static class StateCodes
{
    public static bool TryParse(string? value, out StateCode state)
    {
        state = default;
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        var trimmed = value.Trim();
        return trimmed.All(char.IsLetter)
            && Enum.TryParse(trimmed, ignoreCase: true, out state)
            && Enum.IsDefined(state);
    }
}
