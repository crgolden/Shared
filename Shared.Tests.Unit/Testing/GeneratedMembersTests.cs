namespace Shared.Tests.Unit.Testing;

using System.Globalization;
using System.Reflection;

[Trait("Category", "Unit")]
public sealed class GeneratedMembersTests
{
    public static TheoryData<string> ParameterlessGenerators() =>
        [.. typeof(Generated)
            .GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Where(method => method.GetParameters().Length == 0 && !method.IsGenericMethodDefinition && !method.IsSpecialName)
            .Select(method => method.Name)
            .Distinct(StringComparer.Ordinal)];

    [Theory]
    [MemberData(nameof(ParameterlessGenerators))]
    public void ParameterlessGenerator_Drawn_ReturnsAValueAndNeverAnEmptyString(string generatorName)
    {
        // Arrange
        var generator = typeof(Generated).GetMethod(generatorName, BindingFlags.Public | BindingFlags.Static, Type.EmptyTypes);

        // Act
        var value = generator?.Invoke(null, null);

        // Assert
        Assert.NotNull(value);
        Assert.False(value is string text && text.Length == 0, $"{generatorName} returned an empty string");
    }

    [Fact]
    public void LoopbackHost_Read_IsTheLoopbackAddress()
    {
        // Act
        var host = Generated.LoopbackHost;

        // Assert
        Assert.True(System.Net.IPAddress.IsLoopback(System.Net.IPAddress.Parse(host)));
    }

    [Fact]
    public void LowercaseToken_Generated_DrawsOnlyFromAToZAtTheAskedLength()
    {
        // Arrange
        var length = Generated.NewCount();

        // Act
        var token = Generated.LowercaseToken(length);

        // Assert
        Assert.Matches($"^[a-z]{{{length}}}$", token);
    }

    [Fact]
    public void DigitToken_Generated_DrawsOnlyDigitsAtTheAskedLength()
    {
        // Arrange
        var length = Generated.NewCount();

        // Act
        var token = Generated.DigitToken(length);

        // Assert
        Assert.Matches($"^[0-9]{{{length}}}$", token);
    }

    [Fact]
    public void PunctuationToken_Generated_HoldsNoLetterOrDigit()
    {
        // Arrange
        var length = Generated.NewCount();

        // Act
        var token = Generated.PunctuationToken(length);

        // Assert
        Assert.Equal(length, token.Length);
        Assert.DoesNotMatch("[A-Za-z0-9]", token);
    }

    [Fact]
    public void NewTokenFromCodePointRange_OfOneCodePoint_RepeatsThatCharacter()
    {
        // Arrange
        var letter = Generated.NewLowercaseLetter();
        var codePoint = char.ConvertToUtf32(letter, 0);

        // Act
        var token = Generated.NewTokenFromCodePointRange(codePoint, codePoint);

        // Assert
        Assert.NotEmpty(token);
        Assert.Equal(token.Length, token.Count(character => string.Equals(character.ToString(), letter, StringComparison.Ordinal)));
    }

    [Fact]
    public void NewRoundedFraction_Generated_IsAFractionAtTheAskedScale()
    {
        // Arrange
        var decimalPlaces = CultureInfo.InvariantCulture.NumberFormat.NumberDecimalDigits;

        // Act
        var fraction = Generated.NewRoundedFraction(decimalPlaces);

        // Assert
        Assert.InRange(fraction, decimal.Zero, decimal.One);
        Assert.True(fraction.Scale <= decimalPlaces);
    }

    [Fact]
    public void NewExactlyRepresentableSecondsBelow_Generated_StaysBelowTheLimit()
    {
        // Arrange
        double limit = Generated.NewCount();

        // Act
        var seconds = Generated.NewExactlyRepresentableSecondsBelow(limit);

        // Assert
        Assert.InRange(seconds, double.Epsilon, limit);
        Assert.NotEqual(limit, seconds);
    }

    [Fact]
    public void NewUtcInstantBefore_Generated_PrecedesTheGivenInstant()
    {
        // Arrange
        var instant = Generated.NewUtcInstant();

        // Act
        var earlier = Generated.NewUtcInstantBefore(instant);

        // Assert
        Assert.True(earlier < instant);
    }

    [Fact]
    public void NewDurationShorterThan_Generated_IsPositiveAndBelowTheCeiling()
    {
        // Arrange
        var ceiling = TimeSpan.FromSeconds(Generated.NewCountCeiling());

        // Act
        var duration = Generated.NewDurationShorterThan(ceiling);

        // Assert
        Assert.True(duration > TimeSpan.Zero && duration < ceiling);
    }

    [Fact]
    public void NewTitleId_Generated_CarriesThePrefixThenTheSerialForm()
    {
        // Arrange
        var prefix = Generated.NewUppercaseMarker();

        // Act
        var titleId = Generated.NewTitleId(prefix);

        // Assert
        Assert.Matches($"^{prefix}[0-9]+_00$", titleId);
    }

    [Fact]
    public void NewDistinctTitleIds_Generated_AreDistinctAndPrefixed()
    {
        // Arrange
        var prefix = Generated.NewUppercaseMarker();
        var count = Generated.NewMultiGenreCount();

        // Act
        var titleIds = Generated.NewDistinctTitleIds(prefix, count);

        // Assert
        Assert.Equal(count, titleIds.Distinct(StringComparer.Ordinal).Count());
        Assert.All(titleIds, titleId => Assert.StartsWith(prefix, titleId, StringComparison.Ordinal));
    }

    [Fact]
    public void NewStoreProductId_Generated_EmbedsATitleIdWithThePrefix()
    {
        // Arrange
        var prefix = Generated.NewUppercaseMarker();

        // Act
        var productId = Generated.NewStoreProductId(prefix);

        // Assert
        Assert.Matches($"-{prefix}[0-9]+_00-", productId);
    }

    [Fact]
    public void NewHttpsUri_Generated_IsHttpsOnTheGivenHostAndPath()
    {
        // Arrange
        var host = Generated.NewExternalHost();
        var path = Generated.NewRequestPath();

        // Act
        var uri = Generated.NewHttpsUri(host, path);

        // Assert
        Assert.Equal(Uri.UriSchemeHttps, uri.Scheme);
        Assert.Equal(host, uri.Host);
        Assert.EndsWith(path, uri.AbsolutePath, StringComparison.Ordinal);
    }

    [Fact]
    public void NewHttpUri_Generated_IsPlainHttpOnTheGivenHost()
    {
        // Arrange
        var host = Generated.NewExternalHost();

        // Act
        var uri = Generated.NewHttpUri(host);

        // Assert
        Assert.Equal(Uri.UriSchemeHttp, uri.Scheme);
        Assert.Equal(host, uri.Host);
    }

    [Fact]
    public void NewUriOnHost_Generated_IsOnTheGivenHost()
    {
        // Arrange
        var host = Generated.NewExternalHost();

        // Act
        var uri = Generated.NewUriOnHost(host);

        // Assert
        Assert.Equal(host, uri.Host);
    }

    [Fact]
    public void NewUriWithTraversal_Generated_KeepsAParentSegmentInItsRawText()
    {
        // Arrange
        var host = Generated.NewExternalHost();

        // Act
        var uri = Generated.NewUriWithTraversal(host);

        // Assert
        Assert.Matches(@"/\.\./", uri.OriginalString);
    }

    [Fact]
    public void NewUriWithRawSegment_Generated_KeepsTheSegmentVerbatim()
    {
        // Arrange
        var host = Generated.NewExternalHost();
        var segment = Generated.NewPathSegment();

        // Act
        var uri = Generated.NewUriWithRawSegment(host, segment);

        // Assert
        Assert.Contains($"/{segment}/", uri.OriginalString, StringComparison.Ordinal);
    }

    [Fact]
    public void NewListeningAddress_Generated_UsesTheGivenScheme()
    {
        // Act
        var address = new Uri(Generated.NewListeningAddress(Uri.UriSchemeHttps));

        // Assert
        Assert.Equal(Uri.UriSchemeHttps, address.Scheme);
    }

    [Fact]
    public void NewScoreAtOrAbove_Generated_NeverFallsBelowTheThreshold()
    {
        // Arrange
        var threshold = Generated.NewConfidenceScore();

        // Act
        var score = Generated.NewScoreAtOrAbove(threshold);

        // Assert
        Assert.True(score >= threshold);
    }

    [Fact]
    public void NewScoreBelow_Generated_StaysBelowTheThreshold()
    {
        // Arrange
        var threshold = Generated.NewConfidenceScore();

        // Act
        var score = Generated.NewScoreBelow(threshold);

        // Assert
        Assert.True(score < threshold);
    }

    [Fact]
    public void NewUserAgentTaggedWith_Generated_CarriesTheToken()
    {
        // Arrange
        var token = Generated.NewText();

        // Act
        var userAgent = Generated.NewUserAgentTaggedWith(token);

        // Assert
        Assert.Contains($" {token}/", userAgent, StringComparison.Ordinal);
    }

    [Fact]
    public void NewWebSafeBase64Key_Generated_DecodesToTheAskedLengthWithoutUnsafeCharacters()
    {
        // Arrange
        var length = Generated.NewCount();

        // Act
        var key = Generated.NewWebSafeBase64Key(length);

        // Assert
        Assert.DoesNotMatch("[+/]", key);
        Assert.Equal(length, Convert.FromBase64String(key.Replace('-', '+').Replace('_', '/')).Length);
    }

    [Fact]
    public void NewRandomBytes_Generated_HasTheAskedLength()
    {
        // Arrange
        var length = Generated.NewCount();

        // Act
        var bytes = Generated.NewRandomBytes(length);

        // Assert
        Assert.Equal(length, bytes.Length);
    }

    [Fact]
    public void NewByteSequence_Generated_HasTheAskedLength()
    {
        // Arrange
        var length = Generated.NewCount();

        // Act
        var bytes = Generated.NewByteSequence(length);

        // Assert
        Assert.Equal(length, bytes.Length);
    }

    [Fact]
    public void NewPageAlignedCursor_Generated_IsAPositiveWholeNumberOfPages()
    {
        // Arrange
        var pageSize = Generated.NewPageSize();

        // Act
        var cursor = Generated.NewPageAlignedCursor(pageSize);

        // Assert
        Assert.True(cursor > 0);
        Assert.Equal(0, cursor % pageSize);
    }

    [Fact]
    public void NewGenreList_Generated_HasTheAskedCount()
    {
        // Arrange
        var count = Generated.NewMultiGenreCount();

        // Act
        var genres = Generated.NewGenreList(count);

        // Assert
        Assert.Equal(count, genres.Count);
    }

    [Fact]
    public void NewTitle_WithAPrefix_StartsWithThePrefix()
    {
        // Arrange
        var prefix = Generated.NewText();

        // Act
        var title = Generated.NewTitle(prefix);

        // Assert
        Assert.StartsWith($"{prefix} ", title, StringComparison.Ordinal);
    }

    [Fact]
    public void NewCanonicalTitleContaining_Generated_ContainsTheKeyword()
    {
        // Arrange
        var keyword = Generated.NewKeyword();

        // Act
        var title = Generated.NewCanonicalTitleContaining(keyword);

        // Assert
        Assert.Contains(keyword, title, StringComparison.Ordinal);
    }

    [Fact]
    public void WithAnEditionSuffix_Applied_KeepsTheTitleAndAppendsAWord()
    {
        // Arrange
        var title = Generated.NewLongTitle();

        // Act
        var edition = Generated.WithAnEditionSuffix(title);

        // Assert
        Assert.StartsWith($"{title} ", edition, StringComparison.Ordinal);
        Assert.True(edition.Length > title.Length + 1);
    }

    [Fact]
    public void WithATrailingLetter_Applied_AppendsExactlyOneLetter()
    {
        // Arrange
        var name = Generated.NewText();

        // Act
        var padded = Generated.WithATrailingLetter(name);

        // Assert
        Assert.Matches($"^{name}[a-z]$", padded);
    }

    [Fact]
    public void WithFormattingSeparators_Applied_AddsOnlyASpaceAndAHyphen()
    {
        // Arrange
        var value = Generated.NewText();

        // Act
        var formatted = Generated.WithFormattingSeparators(value);

        // Assert
        Assert.Equal(value, formatted.Replace(" ", string.Empty, StringComparison.Ordinal).Replace("-", string.Empty, StringComparison.Ordinal));
        Assert.NotEqual(value, formatted);
    }

    [Fact]
    public void WithEmbeddedWhitespace_Applied_AddsOnlyWhitespaceInsideTheValue()
    {
        // Arrange
        var value = Generated.NewText();

        // Act
        var spaced = Generated.WithEmbeddedWhitespace(value);

        // Assert
        Assert.Equal(value, string.Concat(spaced.Where(character => !char.IsWhiteSpace(character))));
        Assert.Equal(spaced, spaced.Trim());
    }
}
