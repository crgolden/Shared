namespace Shared.Testing;

using JetBrains.Annotations;

[PublicAPI]
public sealed record DisplayPrice(string Text, int Cents);
