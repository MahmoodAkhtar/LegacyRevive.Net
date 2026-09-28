using System.Globalization;

namespace LegacyRevive.Domain.Artifacts;

public readonly record struct Sha256Hash
{
    private Sha256Hash(string value) => Value = value;

    public string Value { get; }

    public static Sha256Hash FromHex(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (value.Length != 64 || value.Any(character => !Uri.IsHexDigit(character)))
        {
            throw new ArgumentException("A SHA-256 value must contain exactly 64 hexadecimal characters.", nameof(value));
        }

        return new Sha256Hash(value.ToLower(CultureInfo.InvariantCulture));
    }

    public override string ToString() => Value;
}
