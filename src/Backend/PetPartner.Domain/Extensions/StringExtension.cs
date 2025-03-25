using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace PetPartner.Domain.Extensions;

public static class StringExtension
{
    public static bool NotEmpty([NotNullWhen(true)] this string? value) => string.IsNullOrWhiteSpace(value).IsFalse();
}
