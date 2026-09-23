namespace Shared.Testing;

using System.Text.Json;
using JetBrains.Annotations;

[PublicAPI]
public static class JsonPayload
{
    public static string WithNoMembers() => JsonSerializer.Serialize(new { });
}
