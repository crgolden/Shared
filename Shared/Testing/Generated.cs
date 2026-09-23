namespace Shared.Testing;

using System.Data.Common;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using JetBrains.Annotations;
using Shared.Domain;

[PublicAPI]
public static class Generated
{
    private const int SmallestBlankLength = 1;
    private const int LargestBlankLength = 4;
    private const int LargestPrice = 1000;
    private const int PriceDecimalPlaces = 2;
    private const int ProductNameFirstWordLength = 5;
    private const int ProductNameSecondWordLength = 7;
    private const int BrandLength = 6;
    private const int ModelNumberPrefixLength = 3;
    private const int ModelNumberSuffixLength = 5;
    private const int CategoryLength = 8;
    private const int HostLength = 12;
    private const int PathLength = 6;
    private const int SmallestMinutesAgo = 1;
    private const int LargestMinutesAgo = 100000;
    private const int SmallestOffsetHours = 1;
    private const int LargestOffsetHours = 13;
    private const int SmallestZip = 10000;
    private const int LargestZip = 100000;
    private const int CityLength = 9;
    private const int SmallestStreetNumber = 100;
    private const int LargestStreetNumber = 10000;
    private const int StreetNameLength = 10;
    private const int DescriptionFirstWordLength = 7;
    private const int DescriptionSecondWordLength = 9;
    private const int DescriptionThirdWordLength = 5;
    private const int SmallestAreaCode = 200;
    private const int LargestAreaCode = 1000;
    private const int SmallestLineNumber = 1000;
    private const int LargestLineNumber = 10000;
    private const int EmailLocalPartLength = 10;
    private const int EmailDomainLength = 8;
    private const string OneOfEveryCharacterClass = "Aa1!";
    private const int SmallestPasswordTailLength = 6;
    private const int LargestPasswordTailLength = 12;
    private const int DaysInWeek = 7;
    private const int HostLabelLength = 8;
    private const char FirstLetter = 'a';
    private const char FirstLetterOfSecondHalf = 'n';
    private const char LastLetter = 'z';
    private const int NameFirstWordLength = 6;
    private const int NameSecondWordLength = 8;
    private const int SlugFirstWordLength = 6;
    private const int SlugSecondWordLength = 8;
    private const int LanguageLength = 7;
    private const int WebsiteHostLength = 12;
    private const int FailureTokenLength = 10;
    private const int SmallestUnparseableStateCodeLength = 3;
    private const int LargestUnparseableStateCodeLength = 10;
    private const double SmallestLatitude = -90.0;
    private const double LatitudeSpan = 180.0;
    private const double SmallestLongitude = -180.0;
    private const double LongitudeSpan = 360.0;
    private const int CoordinateDecimalPlaces = 6;
    private const int HoursInDay = 24;
    private const int MinutesInHour = 60;
    private const char FirstUppercaseLetter = 'A';
    private const char LastUppercaseLetter = 'Z';
    private const string EnvironmentVariableNamePrefix = "CRGOLDEN_TESTING_";
    private const int EnvironmentVariableNameLength = 12;
    private const int SmallestCount = 1;
    private const int LargestCount = 1000;
    private const int LargestCountCeiling = 10000;
    private const double LargestPercent = 100.0;
    private const int PercentDecimalPlaces = 2;
    private const int SmallestPercentCeiling = 101;
    private const int LargestPercentCeiling = 1000;
    private const int TextLength = 12;
    private const string Punctuation = "!@#$%^&*()-_=+[]{};:,.<>/?|~";
    private const int UspsStateCodeLength = 2;
    private const int SmallestTitleSerial = 10000;
    private const int LargestTitleSerial = 100000;
    private const int SmallestOtherTokenLength = 4;
    private const int LargestOtherTokenLength = 12;
    private const int SmallestUndefinedOffset = 1;
    private const int LargestUndefinedOffset = 100;
    private const int LargestPageMultiple = 200;
    private const int TenthsPerUnit = 10;
    private const int LargestScoreStepAbove = 6;
    private const int LargestScoreStepBelow = 6;
    private const string UserAgentTagVersion = "1.0";

    public static string LowercaseToken(int length) =>
        string.Concat(Enumerable.Range(0, length).Select(_ => (char)Random.Shared.Next(FirstLetter, LastLetter + 1)));

    public static string NewBlank() =>
        new string(' ', Random.Shared.Next(SmallestBlankLength, LargestBlankLength));

    public static string NewProductName() =>
        $"{LowercaseToken(ProductNameFirstWordLength)} {LowercaseToken(ProductNameSecondWordLength)}";

    public static string NewBrand() => LowercaseToken(BrandLength);

    public static string NewModelNumber() =>
        $"{LowercaseToken(ModelNumberPrefixLength)}-{LowercaseToken(ModelNumberSuffixLength)}";

    public static string NewCategory() => LowercaseToken(CategoryLength);

    public static decimal NewPrice() =>
        Math.Round((decimal)(Random.Shared.NextDouble() * LargestPrice), PriceDecimalPlaces);

    public static Uri NewManualUrl() =>
        new Uri($"https://{LowercaseToken(HostLength)}.example/{LowercaseToken(PathLength)}");

    public static Guid NewUserId() => Guid.NewGuid();

    public static DateTimeOffset NewUtcTimestamp() =>
        DateTimeOffset.UtcNow.AddMinutes(-Random.Shared.Next(SmallestMinutesAgo, LargestMinutesAgo));

    public static DateTimeOffset NewTimestampWithNonZeroOffset() =>
        NewUtcTimestamp().ToOffset(TimeSpan.FromHours(-Random.Shared.Next(SmallestOffsetHours, LargestOffsetHours)));

    public static string NewEmailAddress() =>
        $"{LowercaseToken(EmailLocalPartLength)}@{LowercaseToken(EmailDomainLength)}.invalid";

    public static string NewPassword() =>
        OneOfEveryCharacterClass
        + LowercaseToken(Random.Shared.Next(SmallestPasswordTailLength, LargestPasswordTailLength));

    public static string NewPhoneNumber() =>
        string.Create(
            CultureInfo.InvariantCulture,
            $"{Random.Shared.Next(SmallestAreaCode, LargestAreaCode)}-{Random.Shared.Next(SmallestAreaCode, LargestAreaCode)}-{Random.Shared.Next(SmallestLineNumber, LargestLineNumber)}");

    public static string NewCity() => LowercaseToken(CityLength);

    public static string NewStreet() =>
        string.Create(
            CultureInfo.InvariantCulture,
            $"{Random.Shared.Next(SmallestStreetNumber, LargestStreetNumber)} {LowercaseToken(StreetNameLength)} street");

    public static string NewZip() =>
        Random.Shared.Next(SmallestZip, LargestZip).ToString(CultureInfo.InvariantCulture);

    public static string NewDescription() =>
        $"{LowercaseToken(DescriptionFirstWordLength)} {LowercaseToken(DescriptionSecondWordLength)} {LowercaseToken(DescriptionThirdWordLength)}";

    public static byte NewDayOfWeek() => (byte)Random.Shared.Next(0, DaysInWeek);

    public static string NewHostLabel() => LowercaseToken(HostLabelLength);

    public static string NewTokenFromFirstHalfOfAlphabet(int length) =>
        string.Concat(
            Enumerable.Range(0, length).Select(_ => (char)Random.Shared.Next(FirstLetter, FirstLetterOfSecondHalf)));

    public static string NewTokenFromSecondHalfOfAlphabet(int length) =>
        string.Concat(
            Enumerable.Range(0, length).Select(_ => (char)Random.Shared.Next(FirstLetterOfSecondHalf, LastLetter + 1)));

    public static string NewName() => $"{LowercaseToken(NameFirstWordLength)} {LowercaseToken(NameSecondWordLength)}";

    public static string NewSlug() => $"{LowercaseToken(SlugFirstWordLength)}-{LowercaseToken(SlugSecondWordLength)}";

    public static string NewLanguage() => LowercaseToken(LanguageLength);

    public static string NewWebsite() => $"https://{LowercaseToken(WebsiteHostLength)}.example";

    public static string NewFailureMessage() => $"failure-{LowercaseToken(FailureTokenLength)}";

    public static string NewFailureReason() => NewFailureMessage();

    public static string NewUnparseableStateCode() =>
        LowercaseToken(Random.Shared.Next(SmallestUnparseableStateCodeLength, LargestUnparseableStateCodeLength));

    public static double NewLatitude() =>
        Math.Round((Random.Shared.NextDouble() * LatitudeSpan) + SmallestLatitude, CoordinateDecimalPlaces);

    public static double NewLongitude() =>
        Math.Round((Random.Shared.NextDouble() * LongitudeSpan) + SmallestLongitude, CoordinateDecimalPlaces);

    public static TimeOnly NewTimeOfDay() =>
        new TimeOnly(Random.Shared.Next(0, HoursInDay), Random.Shared.Next(0, MinutesInHour));

    public static decimal NewRoundedFraction(int decimalPlaces) =>
        Math.Round((decimal)Random.Shared.NextDouble(), decimalPlaces);

    public static Guid NewChurchId() => Guid.NewGuid();

    public static string UppercaseToken(int length) =>
        string.Concat(
            Enumerable.Range(0, length).Select(_ => (char)Random.Shared.Next(FirstUppercaseLetter, LastUppercaseLetter + 1)));

    public static string NewEnvironmentVariableName() =>
        $"{EnvironmentVariableNamePrefix}{UppercaseToken(EnvironmentVariableNameLength)}";

    public static int NewCount() => Random.Shared.Next(SmallestCount, LargestCount);

    public static int NewCountCeiling() => Random.Shared.Next(LargestCount, LargestCountCeiling);

    public static double NewPercent() =>
        Math.Round(Random.Shared.NextDouble() * LargestPercent, PercentDecimalPlaces);

    public static double NewPercentCeiling() => Random.Shared.Next(SmallestPercentCeiling, LargestPercentCeiling);

    public static string NewText() => LowercaseToken(TextLength);

    public static string NewUnparseableNumber() => LowercaseToken(TextLength);

    public static string Padded(string value) => $"{NewBlank()}{value}{NewBlank()}";

    public static string NewDisplayName() => $"{LowercaseToken(6)} {LowercaseToken(8)}";

    public static string NewClaimValue() => LowercaseToken(Random.Shared.Next(6, 14));

    public static string NewPropertyValue() => LowercaseToken(Random.Shared.Next(6, 14));

    public static string NewSecretValue() => LowercaseToken(Random.Shared.Next(12, 24));

    public static string NewPersonName() => LowercaseToken(Random.Shared.Next(4, 10));

    public static string NewExternalSubject() => Guid.NewGuid().ToString();

    public static string NewPictureAddress() => $"https://{LowercaseToken(10)}.test/{LowercaseToken(6)}.jpg";

    public static string NewLowercaseLetter() => LowercaseToken(1);

    public static string NewGameTitle() => $"{LowercaseToken(5)} {LowercaseToken(8)}";

    public static string NewToken() => $"token-{Guid.NewGuid():N}";

    public static int NewRequestQuota() => Random.Shared.Next(2, 10);

    public static DateTimeOffset NewInstantInsideAMonth() =>
        new(
            2020 + Random.Shared.Next(0, 10),
            Random.Shared.Next(1, 13),
            Random.Shared.Next(1, 28),
            Random.Shared.Next(0, 24),
            Random.Shared.Next(0, 60),
            Random.Shared.Next(0, 60),
            TimeSpan.Zero);

    public static string NewNormalizedTitle() => LowercaseToken(12);

    public static string NewLongTitle() => NewTokenFromFirstHalfOfAlphabet(24);

    public static string WithAnEditionSuffix(string title) =>
        $"{title} {NewTokenFromSecondHalfOfAlphabet(title.Length / 4)}";

    public static string NewPublisher() => $"{LowercaseToken(6)} {LowercaseToken(9)}";

    public static string NewGenre() => LowercaseToken(9).ToUpperInvariant();

    public static string NewTagWithoutAMultiplayerKeyword() => NewTokenFromFirstHalfOfAlphabet(9);

    public static string NewFranchiseName() => $"{LowercaseToken(4)} {LowercaseToken(7)}";

    public static int NewRulePriority() => Random.Shared.Next(1, 100);

    public static int NewGenrePriorityRank() => Random.Shared.Next(0, 10);

    public static int NewPositiveRankGap() => Random.Shared.Next(1, 10);

    public static string NewFingerprint() => $"fingerprint-{Guid.NewGuid():N}";

    public static string NewOpenCriticTier() => $"tier-{LowercaseToken(8)}";

    public static string NewContentRating() => $"rating-{LowercaseToken(6)}";

    public static string NewRatingAuthority() => $"authority-{LowercaseToken(8)}";

    public static string NewCoverImageAddress() => $"https://{LowercaseToken(10)}.example/{LowercaseToken(8)}.png";

    public static Uri NewCoverImageUri() => new(NewCoverImageAddress(), UriKind.Absolute);

    public static string NewTitleIdWithPrefix(string prefix) => $"{prefix}{Random.Shared.Next(10000, 100000)}_00";

    public static string NewTextShorterThanATitleIdPrefix() => LowercaseToken(Random.Shared.Next(1, 4)).ToUpperInvariant();

    public static IReadOnlyList<string> NewDistinctTitleIds(int count)
    {
        var firstSerial = Random.Shared.Next(10000, 100000 - count);
        return [.. Enumerable.Range(0, count).Select(offset => NewTitleIdWithSerial(firstSerial + offset))];
    }

    public static int NewConceptNumericId() => Random.Shared.Next(1, 100_000_000);

    public static string NewConceptType() => $"concepttype-{LowercaseToken(6)}";

    public static string NewReleaseDateType() => $"releasetype-{LowercaseToken(6)}";

    public static string NewContentRatingDescription() => $"rated {LowercaseToken(8)}";

    public static string NewImageType() => $"imagetype-{LowercaseToken(6)}";

    public static string NewCompatibilityNoticeType() => $"noticetype-{LowercaseToken(6)}";

    public static string NewNonNumericToken() => $"count-{LowercaseToken(8)}";

    public static int NewMultiplayerPlayerCount() => Random.Shared.Next(2, 100);

    public static int NewMinimumAge() => Random.Shared.Next(0, 21);

    public static DateTimeOffset NewReleaseTimestamp() =>
        new DateTimeOffset(DateTimeOffset.UtcNow.UtcDateTime.Date, TimeSpan.Zero)
            .AddDays(-Random.Shared.Next(1, 3_650));

    public static int NewExpiresInSeconds() => Random.Shared.Next(60, 86_400);

    public static int NewRateLimitMaxRequests() => Random.Shared.Next(2, 20);

    public static double NewRateLimitWindowSeconds() => Random.Shared.Next(30, 900);

    public static double NewFractionAboveHalf() => 0.55 + (Random.Shared.NextDouble() * 0.4);

    public static int NewSecondsUntilTheWindowHasRoom() => Random.Shared.Next(1, 30);

    public static int NewSecondsAgoInsideAMinuteWindow() => Random.Shared.Next(1, 60);

    public static int NewSecondsInsideDecember() => Random.Shared.Next(1, (int)TimeSpan.FromDays(31).TotalSeconds + 1);

    public static int NewSmallTokenEstimate() => Random.Shared.Next(1, 100);

    public static Guid NewOpenAICallId() => Guid.NewGuid();

    public static double NewExactlyRepresentableSecondsBelow(double limit) =>
        Random.Shared.Next(1, (int)(limit * 1024)) / 1024.0;

    public static TimeSpan NewNonZeroUtcOffset() => TimeSpan.FromHours(Random.Shared.Next(1, 13));

    public static IReadOnlyList<string> NewGenreList(int count) =>
        [.. Enumerable.Range(0, count).Select(_ => NewGenre())];

    public static string NewConceptId() =>
        Random.Shared.Next(10_000_000, 100_000_000).ToString(CultureInfo.InvariantCulture);

    public static string NewConceptIdSortingFirst() =>
        Random.Shared.Next(10_000_000, 50_000_000).ToString(CultureInfo.InvariantCulture);

    public static string NewConceptIdSortingLast() =>
        Random.Shared.Next(50_000_000, 100_000_000).ToString(CultureInfo.InvariantCulture);

    public static string NewOverrideName() => $"override {LowercaseToken(10)}";

    public static string NewEditionKeyword() => $"{LowercaseToken(4)} {LowercaseToken(6)}";

    public static int NewEditionRank() => Random.Shared.Next(1, 10);

    public static string NewEntitlementId() => $"entitlement-{Guid.NewGuid():N}";

    public static string NewProductId() => $"product-{Guid.NewGuid():N}";

    public static string NewSkuId() => $"sku-{Guid.NewGuid():N}";

    public static string NewPackageType() => $"package-{LowercaseToken(4)}";

    public static string NewGameType() => $"gametype-{LowercaseToken(4)}";

    public static string NewPlatformId() => $"platform-{LowercaseToken(4)}";

    public static long NewDownloadSizeBytes() => Random.Shared.NextInt64(1, 50L * 1024 * 1024 * 1024);

    public static string NewPs3EntitlementId() =>
        $"UP{DigitToken(4)}-BLUS{Random.Shared.Next(10000, 100000)}_00-{LowercaseToken(16).ToUpperInvariant()}";

    public static string NewNonTitleEntitlementId() =>
        $"IP{DigitToken(4)}-NPIA{Random.Shared.Next(10000, 100000)}_00-{LowercaseToken(16).ToUpperInvariant()}";

    public static string NewEntitlementIdPrefix() => $"{LowercaseToken(6)}-";

    public static string NewJsonPropertyName() => LowercaseToken(9);

    public static string NewBlankRun() => new(' ', Random.Shared.Next(1, 4));

    public static DateTimeOffset NewNewYearsMidnightAheadOfUtc() =>
        new DateTimeOffset(NewReleaseYear(), 1, 1, 0, 0, 0, NewNonZeroUtcOffset());

    public static string NewNpCommunicationId() => $"NPWR{Random.Shared.Next(10000, 100000)}_00";

    public static string NewAccessToken() => $"access-{Guid.NewGuid():N}";

    public static string NewRefreshToken() => $"refresh-{Guid.NewGuid():N}";

    public static string NewNpsso() => $"npsso-{Guid.NewGuid():N}";

    public static string NewAuthorizationCode() => $"code-{Guid.NewGuid():N}";

    public static string NewRequestPath() => $"path-{Guid.NewGuid():N}";

    public static string NewRapidApiKey() => $"rapidapi-key-{Guid.NewGuid():N}";

    public static string NewRawgApiKey() => $"rawg-key-{Guid.NewGuid():N}";

    public static string NewOpenAIApiKey() => $"openai-key-{Guid.NewGuid():N}";

    public static string NewRedisPassword() => $"redis-password-{Guid.NewGuid():N}";

    public static string NewResendApiToken() => $"resend-token-{Guid.NewGuid():N}";

    public static string NewPostgresConnectionString() =>
        new DbConnectionStringBuilder
        {
            ["Host"] = NewHostLabel(),
            ["Database"] = NewLettersOnlyToken(),
            ["Username"] = NewLettersOnlyToken(),
            ["Password"] = NewToken(),
        }.ConnectionString;

    public static Uri NewProviderBaseAddress() =>
        new UriBuilder(Uri.UriSchemeHttps, NewHostLabel()) { Path = "/" }.Uri;

    public static Uri NewProviderBaseAddressUnderAPathPrefix() =>
        new UriBuilder(Uri.UriSchemeHttps, NewHostLabel()) { Path = $"/{LowercaseToken(4)}/" }.Uri;

    public static int NewRetryAfterSeconds() => Random.Shared.Next(1, 600);

    public static int NewEntitlementsBeyondTheLimit() => Random.Shared.Next(1, 10_000);

    public static long NewUnexpiredAccessTokenExpiry() =>
        DateTimeOffset.UtcNow.AddSeconds(Random.Shared.Next(600, 90_000)).ToUnixTimeSeconds();

    public static long NewStoredRefreshTokenExpiry() =>
        DateTimeOffset.UtcNow.AddDays(Random.Shared.Next(1, 60)).ToUnixTimeSeconds();

    public static HttpStatusCode NewClientErrorStatusCode() => (HttpStatusCode)Random.Shared.Next(400, 500);

    public static HttpStatusCode NewServerErrorStatusCode() => (HttpStatusCode)Random.Shared.Next(500, 600);

    public static int NewMultiGenreCount() => Random.Shared.Next(2, 5);

    public static string NewPunctuationRun() => new('!', Random.Shared.Next(2, 6));

    public static Uri NewUriOnHost(string host) =>
        new UriBuilder(Uri.UriSchemeHttps, host) { Path = NewRequestPath() }.Uri;

    public static string NewUpstreamErrorBody() => $"upstream-failure-{Guid.NewGuid():N}";

    public static string NewRejectionMessage() => $"rejected-{Guid.NewGuid():N}";

    public static int NewRefreshTokenExpiresInSeconds() => Random.Shared.Next(86_400, 5_184_000);

    public static string NewGameName() => $"Game {Guid.NewGuid():N}";

    public static Guid NewIdentitySub() => Guid.NewGuid();

    public static int NewRawgGameId() => Random.Shared.Next(1, 1_000_000);

    public static double NewOpenCriticScore() => Random.Shared.Next(0, 1001) / 10.0;

    public static int NewOpenCriticGameId() => Random.Shared.Next(1, 1_000_000);

    public static double NewCriticScore() => Math.Round(Random.Shared.NextDouble() * 100.0, 2);

    public static double NewPercentRecommended() => Math.Round(Random.Shared.NextDouble() * 100.0, 2);

    public static double NewStarRating() => Math.Round(Random.Shared.NextDouble() * 5.0, 2);

    public static int NewPsnRatingCount() => Random.Shared.Next(1, 1_000_000);

    public static string NewGenreDisplayName() => $"{LowercaseToken(6)} {LowercaseToken(7)}";

    public static DateOnly NewReleaseDate() =>
        new DateOnly(2000, 1, 1).AddDays(Random.Shared.Next(0, 9_000));

    public static int NewReleaseYear() => Random.Shared.Next(1990, 2030);

    public static string NewErrorMessage() => $"failure-{LowercaseToken(10)}";

    public static string NewSettingKey() => $"Setting{Guid.NewGuid():N}";

    public static Guid NewContributorId() => Guid.NewGuid();

    public static string NewFieldName() => $"field{Guid.NewGuid():N}";

    public static string NewFieldValue() => $"value{Guid.NewGuid():N}";

    public static int NewPortNumber() => Random.Shared.Next(1024, 65535);

    public static string NewEmailSubject() => $"Subject {Guid.NewGuid():N}";

    public static string NewHtmlBody() => $"<p>{Guid.NewGuid():N}</p>";

    public static string NewGroupKey() => $"group-{Guid.NewGuid():N}";

    public static Guid NewGameId() => Guid.NewGuid();

    public static int NewTrophyProgress() => Random.Shared.Next(1, 100);

    public static string NewPublisherPattern() => LowercaseToken(8);

    public static string NewChurchName() => $"church{LowercaseToken(12)}";

    public static string NewNonLatinChurchName() =>
        string.Concat(Enumerable.Range(0, 8).Select(_ => (char)Random.Shared.Next(0x4E00, 0x9FFF)));

    public static string NewStreetName() => $"{Guid.NewGuid():N} Street";

    public static string NewHouseNumber() =>
        Random.Shared.Next(100, 9999).ToString(CultureInfo.InvariantCulture);

    public static string NewImportBlobPath() => $"{Guid.NewGuid():N}/{Guid.NewGuid():N}";

    public static string WithATrailingLetter(string name) => $"{name}{NewPaddingChar()}";

    public static int NewChurchCountSharingABucket() => Random.Shared.Next(3, 41);

    public static double NewOffsetWithinHalfACell() => Random.Shared.Next(1, 50) / 100.0;

    public static string NewLettersOnlyToken() => LowercaseToken(8);

    public static string NewDigitsOnlyToken() => DigitToken(8);

    public static string NewDigitsOnlyName() => DigitToken(16);

    public static string DigitToken(int length) =>
        string.Concat(Enumerable.Range(0, length).Select(_ => (char)Random.Shared.Next('0', '9' + 1)));

    public static string NewCampusName() => $"campus{LowercaseToken(12)}";

    public static string NewFullStateName() => $"state{LowercaseToken(10)}";

    public static string NewNteeCode() =>
        $"X{Random.Shared.Next(10, 100).ToString(CultureInfo.InvariantCulture)}";

    public static byte NewEarlyWeekDayOfWeek() => (byte)Random.Shared.Next(0, 3);

    public static byte NewLateWeekDayOfWeek() => (byte)Random.Shared.Next(3, 7);

    public static byte NewInvalidDayOfWeek() => (byte)Random.Shared.Next(7, 256);

    public static char NewPaddingChar() => (char)Random.Shared.Next('a', 'z' + 1);

    public static int NewOverflowMargin() => Random.Shared.Next(1, 100);

    public static decimal NewConfidence() => Math.Round((decimal)Random.Shared.NextDouble(), 2);

    public static int NewWorshipStyleCode() =>
        Random.Shared.Next(ChurchBuilder.MinWorshipStyle, ChurchBuilder.MaxWorshipStyle + 1);

    public static string NewStateCodeText() =>
        $"{(char)Random.Shared.Next('A', 'Z' + 1)}{(char)Random.Shared.Next('A', 'Z' + 1)}";

    public static string NewOverlongZipDigits() =>
        Random.Shared.NextInt64(100_000_000_000L, 1_000_000_000_000L)
            .ToString(CultureInfo.InvariantCulture);

    public static string NewParenthesizedPhoneNumber() =>
        $"({Random.Shared.Next(200, 1000)}) {Random.Shared.Next(200, 1000)}-{Random.Shared.Next(1000, 10000)}";

    public static string NewProseWithoutJson() => $"prose {LowercaseToken(10)} {LowercaseToken(6)}";

    public static string NewProseWithoutAPhoneNumber() =>
        $"{LowercaseToken(6)} {LowercaseToken(7)} {LowercaseToken(10)}";

    public static string NewBlobPath() => $"{LowercaseToken(2)}/{LowercaseToken(10)}.html";

    public static string NewHost() => $"host{LowercaseToken(12)}.example";

    public static string NewMinistryName() => $"ministry{LowercaseToken(12)}";

    public static string NewMinistryDescription() => $"description{LowercaseToken(12)}";

    public static string NewChurchServiceDescription() => $"service{LowercaseToken(12)}";

    public static string NewDenominationName() => $"denomination{LowercaseToken(12)}";

    public static string NewLanguageName() => $"language{LowercaseToken(8)}";

    public static string NewServiceTime() =>
        $"{Random.Shared.Next(0, 24):D2}:{Random.Shared.Next(0, 60):D2}";

    public static decimal NewGeocodedLatitude() =>
        Math.Round(((decimal)Random.Shared.NextDouble() * 40m) + 1m, 4);

    public static decimal NewGeocodedLongitude() =>
        -Math.Round(((decimal)Random.Shared.NextDouble() * 100m) + 1m, 4);

    public static double NewScoredLatitude() => Math.Round((Random.Shared.NextDouble() * 40) + 1, 4);

    public static double NewScoredLongitude() => -Math.Round((Random.Shared.NextDouble() * 100) + 1, 4);

    public static Guid NewCrawlSourceId() => Guid.NewGuid();

    public static decimal NewOutOfRangeLatitude() => Random.Shared.Next(91, 1000);

    public static int NewOutOfRangeWorshipStyle() =>
        ChurchBuilder.MaxWorshipStyle + Random.Shared.Next(SmallestUndefinedOffset, LargestUndefinedOffset);

    public static TimeSpan NewJobTimeBudgetAllowance() => TimeSpan.FromSeconds(Random.Shared.Next(60, 3_600));

    public static int NewJobRunSeq() => Random.Shared.Next(0, 1000);

    public static int NewBatchLimitAboveAFewCandidates() => Random.Shared.Next(10, 1000);

    public static int NewPreviousSitemapChunkCount() => Random.Shared.Next(2, 10);

    public static string NewRawgReleasedText() =>
        NewReleaseDate().ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

    public static double NewMetacriticScore() => Random.Shared.Next(1, 101);

    public static int NewRawgPlatformId() => Random.Shared.Next(1, 1_000);

    public static string NewRawgPlatformName() => $"platform{Guid.NewGuid():N}";

    public static string NewEsrbRatingName() => $"esrb{Guid.NewGuid():N}";

    public static string NewProviderErrorBody() => JsonSerializer.Serialize(new { detail = NewErrorMessage() });

    public static string NewOpenCriticRawPayload() => JsonSerializer.Serialize(new { id = NewOpenCriticGameId() });

    public static string NewUnknownSslModeSpelling() => $"sslmode{Guid.NewGuid():N}";

    public static string NewPostgresIdentifier() => $"id{Guid.NewGuid():N}";

    public static byte[] NewCiphertext() => Guid.NewGuid().ToByteArray();

    public static long NewAccessTokenExpiry() => 1_700_000_000 + Random.Shared.Next(1, 100_000);

    public static long NewRefreshTokenExpiry() => 1_800_000_000 + Random.Shared.Next(1, 100_000);

    public static int NewReGeocodeBatchSize() => Random.Shared.Next(10, 500);

    public static Guid NewRunId() => Guid.NewGuid();

    public static int NewConsecutiveFailureCount() => Random.Shared.Next(2, 20);

    public static string NewHtmlDocument() => $"<html><h1>{Guid.NewGuid():N}</h1></html>";

    public static string NewPlaintextSecret() => $"secret-{Guid.NewGuid():N}";

    public static string NewMalformedJson() => $"{{{LowercaseToken(8)}";

    public static TimeSpan NewHeartbeatInterval() => TimeSpan.FromSeconds(Random.Shared.Next(1, 3_600));

    public static Guid NewPublisherTierRuleId() => Guid.NewGuid();

    public static Guid NewFranchiseRuleId() => Guid.NewGuid();

    public static Guid NewEntitlementPullId() => Guid.NewGuid();

    public static Guid NewCampusId() => Guid.NewGuid();

    public static TimeSpan NewScheduleDriftBeyondTolerance() => TimeSpan.FromMinutes(Random.Shared.Next(1, 10_080));

    public static string PunctuationToken(int length) =>
        string.Concat(Enumerable.Range(0, length).Select(_ => Punctuation[Random.Shared.Next(Punctuation.Length)]));

    public static string NewKeyword() => LowercaseToken(Random.Shared.Next(4, 12));

    public static string NewPunctuationToken() => PunctuationToken(Random.Shared.Next(1, 4));

    public static string NewPunctuationOnlyQuery() =>
        $"{NewPunctuationToken()} {NewPunctuationToken()}";

    public static string NewUnrecognizedStateCode()
    {
        string candidate;
        do
        {
            candidate = string.Concat(
                Enumerable.Range(0, UspsStateCodeLength).Select(_ => (char)Random.Shared.Next('A', 'Z' + 1)));
        }
        while (StateCodes.TryParse(candidate, out _));

        return candidate;
    }

    public static Uri NewWebsiteUri() => new Uri(NewWebsite());

    public static StateCode NewStateCode()
    {
        var defined = Enum.GetValues<StateCode>();
        return defined[Random.Shared.Next(defined.Length)];
    }

    public static string NewNonGuidSubject() => $"user-{LowercaseToken(12)}";

    public static double NewRadiusMiles() => Math.Round(Random.Shared.NextDouble() * 500.0, 2);

    public static double NewLatitudeBeyondThePole() => 90.0 + Math.Round(Random.Shared.NextDouble() * 90.0, 6) + 1;

    public static int NewRowCount() => Random.Shared.Next(1, 1000);

    public static int NewPage() => Random.Shared.Next(1, 50);

    public static int NewPageSize() => Random.Shared.Next(1, 100);

    public static TimeOnly NewTimeOfDayBeforeTheLastHour() => new TimeOnly(Random.Shared.Next(0, 23), Random.Shared.Next(0, 60));

    public static decimal NewConfidenceScore() => Math.Round((decimal)Random.Shared.NextDouble(), 4);

    public static string NewNonUspsLetterPair()
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

    public static string NewAttributeKey() => LowercaseToken(11);

    public static string NewAttributeSource() => LowercaseToken(10);

    public static decimal NewOutOfRangeOffset() => Random.Shared.Next(1, 1001) / 10000m;

    public static string NewUserName() => LowercaseToken(9);

    public static string NewTaggedEmailAddress() =>
        $"{LowercaseToken(6)}+{LowercaseToken(4)}@{LowercaseToken(8)}.example";

    public static string NewNonAsciiEmailAddress() =>
        $"{LowercaseToken(5)}ä{LowercaseToken(4)}@{LowercaseToken(7)}.example";

    public static string NewProviderKey() => LowercaseToken(16);

    public static string NewClaimType() => LowercaseToken(7);

    public static string NewRoleName() => LowercaseToken(8);

    public static string NewGivenName() =>
        $"{(char)Random.Shared.Next('A', 'Z' + 1)}{LowercaseToken(7)}";

    public static int NewEntityId() => Random.Shared.Next(1, 100_000);

    public static string NewRequestId() => LowercaseToken(12);

    public static string NewClientIdentifier() => $"client-{LowercaseToken(10)}";

    public static string NewClientName() => $"{LowercaseToken(5)} {LowercaseToken(7)}";

    public static string NewApiResourceName() => $"api-{LowercaseToken(8)}";

    public static string NewScopeName() => $"scope-{LowercaseToken(8)}";

    public static string NewApiScopeName() => $"{NewApiResourceName()}.{LowercaseToken(5)}";

    public static string NewPropertyKey() => LowercaseToken(9);

    public static string NewGrantType() => LowercaseToken(11);

    public static string NewExternalHost() => $"{LowercaseToken(10)}.example";

    public static string NewOrigin() => Uri.UriSchemeHttps + Uri.SchemeDelimiter + NewExternalHost();

    public static string NewCallbackAddress() => $"{NewOrigin()}/{LowercaseToken(7)}";

    public static string NewSchemeName() => LowercaseToken(10);

    public static string NewNamespacedEntityId() => $"urn:{LowercaseToken(6)}:{LowercaseToken(8)}";

    public static string NewSessionKey() => Guid.NewGuid().ToString("N");

    public static string NewSubjectId() => Guid.NewGuid().ToString();

    public static string NewNumericSubjectId() =>
        string.Concat(Enumerable.Range(0, 21).Select(_ => (char)Random.Shared.Next('0', '9' + 1)));

    public static string NewLocalPath() => '/' + LowercaseToken(8);

    public static string NewEmailConfirmationToken() => Guid.NewGuid().ToString("N");

    public static string NewModelStateKey() => LowercaseToken(7);

    public static int NewRecoveryCodeCount() => Random.Shared.Next(1, 11);

    public static string NewValidationMessage() => $"{LowercaseToken(6)} {LowercaseToken(9)}";

    public static string NewFirstAlphabeticalName() => NewTokenFromFirstHalfOfAlphabet(9);

    public static string NewLastAlphabeticalName() => NewTokenFromSecondHalfOfAlphabet(9);

    public static string NewActivitySourceName() => $"{LowercaseToken(6)}.{LowercaseToken(6)}";

    public static string NewActivityName() => $"{LowercaseToken(5)}-{LowercaseToken(9)}";

    public static string NewKeyId() => Guid.NewGuid().ToString("N");

    public static string NewSigningAlgorithmName() => $"RS{Random.Shared.Next(256, 513)}";

    public static string NewLogoutId() => Guid.NewGuid().ToString("N");

    public static string NewReferenceValueHash() => Guid.NewGuid().ToString("N");

    public static byte[] NewByteSequence(int length)
    {
        var bytes = new byte[length];
        Random.Shared.NextBytes(bytes);
        return bytes;
    }

    public static byte[] NewIPv4AddressBytes() => NewByteSequence(4);

    public static byte[] NewCredentialIdBytes() => NewByteSequence(16);

    public static byte[] NewPublicKeyBytes() => NewByteSequence(32);

    public static byte[] NewAttestationObjectBytes() => NewByteSequence(24);

    public static byte[] NewClientDataJsonBytes() => NewByteSequence(26);

    public static string NewTenantName() => LowercaseToken(8);

    public static string NewButtonValue() => LowercaseToken(6);

    public static string NewPasskeyAction() => LowercaseToken(6);

    public static string NewAttributeName() => LowercaseToken(7);

    public static string NewAttributeValue() => LowercaseToken(10);

    public static string NewButtonLabel() => LowercaseToken(9);

    public static string NewPolicyDirectiveSource() => LowercaseToken(9);

    public static string NewOverlongDisplayName() => LowercaseToken(80);

    public static string NewRecoveryCode() => Guid.NewGuid().ToString("N");

    public static string NewAuthenticatorKey() => Guid.NewGuid().ToString("N").ToUpperInvariant();

    public static string NewVerificationCode() =>
        Random.Shared.Next(0, 1_000_000).ToString("D6", System.Globalization.CultureInfo.InvariantCulture);

    public static string NewPageName() => NewTokenFromFirstHalfOfAlphabet(8);

    public static string NewDifferentPageName() => NewTokenFromSecondHalfOfAlphabet(8);

    public static string NewPathSegment() => LowercaseToken(6);

    public static string NewAdminSectionPage() => $"/{NewPathSegment()}/{NewPageName()}/{NewPathSegment()}";

    public static string NewPunctuatedPageName() =>
        LowercaseToken(6) + string.Concat(Enumerable.Range(0, 4).Select(_ => (char)Random.Shared.Next('!', '-')));

    public static string NewOverlongPageName() =>
        new((char)Random.Shared.Next('a', 'z' + 1), Random.Shared.Next(600, 1_200));

    public static string NewWhitespaceValue() => new(' ', Random.Shared.Next(1, 6));

    public static string WithFormattingSeparators(string value)
    {
        var firstBreak = Random.Shared.Next(1, value.Length);
        var secondBreak = Random.Shared.Next(firstBreak, value.Length);
        return string.Concat(value[..firstBreak], ' ', value[firstBreak..secondBreak], '-', value[secondBreak..]);
    }

    public static string WithEmbeddedWhitespace(string value) =>
        value.Insert(Random.Shared.Next(1, value.Length), NewWhitespaceValue());

    public static string NewTokenFromCodePointRange(int firstCodePoint, int lastCodePoint) =>
        string.Concat(Enumerable
            .Range(0, Random.Shared.Next(6, 12))
            .Select(_ => char.ConvertFromUtf32(Random.Shared.Next(firstCodePoint, lastCodePoint + 1))));

    public static string NewOverlongValue() =>
        new((char)Random.Shared.Next('a', 'z' + 1), Random.Shared.Next(5_000, 10_000));

    public static string NewControlAndSymbolValue() =>
        NewPunctuatedPageName() + '\0' + '\n' + '\t' + '☃';

    public static DateTimeOffset NewUtcInstant() =>
        DateTimeOffset.UtcNow.AddMinutes(-Random.Shared.Next(1, 100_000));

    public static DateTime NewUtcDateTime() => NewUtcInstant().UtcDateTime;

    public static DateTimeOffset NewUtcInstantBefore(DateTimeOffset instant) =>
        instant.AddMinutes(-Random.Shared.Next(1, 100_000));

    public static string NewBrowserUserAgent() => $"Mozilla/5.0 ({LowercaseToken(12)}) {LowercaseToken(8)}/1.0";

    public static string NewPasskeyOptionsJson() => $"{{\"challenge\":\"{Guid.NewGuid():N}\"}}";

    public static string NewPasskeyCredentialJson() =>
        $"{{\"id\":\"{LowercaseToken(16)}\",\"type\":\"public-key\"}}";

    public static string NewRecaptchaToken() => LowercaseToken(16);

    public static string NewRecaptchaSecretKey() => LowercaseToken(24);

    public static string NewRecaptchaSiteKey() => LowercaseToken(12);

    public static string NewTitle() => Guid.NewGuid().ToString();

    public static string NewTitle(string prefix) => $"{prefix} {Guid.NewGuid():N}";

    public static string NewCanonicalTitle() => NewTitle(LowercaseToken(5).ToUpperInvariant());

    public static string NewCanonicalTitleContaining(string keyword) =>
        NewTitle($"{LowercaseToken(5).ToUpperInvariant()} {keyword}");

    public static string NewPs3TitleId() =>
        $"BLUS{Random.Shared.Next(10000, 100000).ToString(System.Globalization.CultureInfo.InvariantCulture)}_00";

    public static string NewOwnedEdition() => $"edition-{LowercaseToken(8)}";

    public static string NewFranchiseKeyword() => Guid.NewGuid().ToString("N");

    public static int NewJobSeq() => Random.Shared.Next(1, 100);

    public static TimeSpan NewLiveLease() => TimeSpan.FromMinutes(Random.Shared.Next(5, 60));

    public static DateTimeOffset NewUtcTimestampAtSecondPrecision() =>
        new DateTimeOffset(DateTimeOffset.UtcNow.UtcDateTime.Date, TimeSpan.Zero)
            .AddDays(-Random.Shared.Next(1, 3_650))
            .AddSeconds(Random.Shared.Next(0, 86_400));

    public static decimal NewStoredCriticScore() => decimal.Round((decimal)Random.Shared.NextDouble() * 100m, 2);

    public static decimal NewStoredPercentRecommended() => decimal.Round((decimal)Random.Shared.NextDouble() * 100m, 2);

    public static string NewTransportFailureMessage() => $"transport-failure-{LowercaseToken(10)}";

    public static string NewMonitoredServiceDescription() => $"description-{LowercaseToken(10)}";

    public static string NewServiceAddress() => $"https://{LowercaseToken(12)}.example";

    public static string LoopbackHost => System.Net.IPAddress.Loopback.ToString();

    public static string NewUnexpectedHealthBody() => LowercaseToken(8);

    public static string NewHostname() => $"{LowercaseToken(12)}.example";

    public static string NewListeningAddress(string scheme) =>
        $"{scheme}{Uri.SchemeDelimiter}{NewHostname()}:{Random.Shared.Next(1_024, 65_536)}";

    public static Microsoft.AspNetCore.Http.PathString NewCallbackPath() => new($"/{LowercaseToken(8)}");

    public static TimeSpan NewPingInterval() => TimeSpan.FromSeconds(Random.Shared.Next(1, 3_600));

    public static string NewMonitoredServiceName() => $"{LowercaseToken(5)} {LowercaseToken(7)}";

    public static string NewDatabaseName() => LowercaseToken(8);

    public static string NewSqlLogin() => LowercaseToken(6);

    public static int NewClosedLoopbackPort()
    {
        using var probe = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, 0);
        probe.Start();
        var port = ((System.Net.IPEndPoint)probe.LocalEndpoint).Port;
        probe.Stop();
        return port;
    }

    public static string NewChatTitle() => $"{LowercaseToken(5)} {LowercaseToken(7)}";

    public static string NewModelName() => $"{LowercaseToken(4)}-{LowercaseToken(4)}";

    public static string NewMaxOutputTokenCount() =>
        Random.Shared.Next(256, 4096).ToString(System.Globalization.CultureInfo.InvariantCulture);

    public static string NewInstructions() => $"{LowercaseToken(8)} {LowercaseToken(6)}";

    public static double NewSortedSetScore() => Random.Shared.Next(1, 1_000_000);

    public static long NewRedisListLength() => Random.Shared.Next(1, 100);

    public static string NewResponseId() => $"resp_{LowercaseToken(12)}";

    public static string NewNonGuidSortedSetMember() => LowercaseToken(12);

    public static string NewUnparseableTimestamp() => LowercaseToken(9);

    public static string NewMessageText() => $"{LowercaseToken(6)} {LowercaseToken(9)}";

    public static int NewLatencyMilliseconds() => Random.Shared.Next(1, 1000);

    public static string NewRequestActivityName() => $"GET /{LowercaseToken(6)}";

    public static string NewWorkActivityName() => $"{LowercaseToken(7)}.{LowercaseToken(5)}.{LowercaseToken(6)}";

    public static long NewUnixSeconds() =>
        DateTimeOffset.UtcNow.AddSeconds(-Random.Shared.Next(1, 10_000_000)).ToUnixTimeSeconds();

    public static string NewProductModel() =>
        $"{LowercaseToken(2).ToUpperInvariant()}{Random.Shared.Next(10, 100)}{LowercaseToken(1).ToUpperInvariant()}";

    public static string NewOpenApiDocumentName() => LowercaseToken(4);

    public static string NewRoutePrefix() => LowercaseToken(5);

    public static string NewEntitySetName() => LowercaseToken(9);

    public static string NewMalformedGuid() => $"{LowercaseToken(8)}-{LowercaseToken(4)}";

    public static string NewUppercaseMarker() => LowercaseToken(4).ToUpperInvariant();

    public static string NewTimeoutMessage() => $"timeout-{LowercaseToken(10)}";

    public static string NewUppercaseSortingName() => $"Z{NewProductName()}";

    public static string NewLowercaseSortingName() => $"a{NewProductName()}";

    public static string NewSettingToken() => Guid.NewGuid().ToString("N");

    public static int NewEventIdentifier() => Random.Shared.Next();

    public static T NewDefinedValue<T>()
        where T : struct, Enum
    {
        var defined = Enum.GetValues<T>();
        return defined[Random.Shared.Next(defined.Length)];
    }

    public static T NewUndefinedValue<T>()
        where T : struct, Enum
    {
        var largest = Enum.GetValues<T>().Max(value => Convert.ToInt32(value, CultureInfo.InvariantCulture));
        return (T)Enum.ToObject(typeof(T), largest + Random.Shared.Next(SmallestUndefinedOffset, LargestUndefinedOffset));
    }

    public static string NewTokenOtherThan(params string[] excluded)
    {
        ArgumentNullException.ThrowIfNull(excluded);
        string candidate;
        do
        {
            candidate = LowercaseToken(Random.Shared.Next(SmallestOtherTokenLength, LargestOtherTokenLength));
        }
        while (excluded.Contains(candidate, StringComparer.Ordinal));

        return candidate;
    }

    public static TimeSpan NewDurationShorterThan(TimeSpan ceiling) =>
        TimeSpan.FromSeconds(Random.Shared.Next(1, (int)ceiling.TotalSeconds));

    public static string NewTitleId(string platformPrefix) =>
        NewTitleIdWithSerial(platformPrefix, Random.Shared.Next(SmallestTitleSerial, LargestTitleSerial));

    public static string NewTitleIdWithSerial(string platformPrefix, int serial) => $"{platformPrefix}{serial}_00";

    public static string NewStoreProductId(string platformPrefix) =>
        $"UP{DigitToken(4)}-{NewTitleId(platformPrefix)}-{LowercaseToken(16).ToUpperInvariant()}";

    public static byte[] NewRandomBytes(int length)
    {
        var bytes = new byte[length];
        RandomNumberGenerator.Fill(bytes);
        return bytes;
    }

    public static string NewUrlSafeBase64Key(int length) =>
        Convert.ToBase64String(NewRandomBytes(length)).Replace('+', '-').Replace('/', '_');

    public static int NewPageAlignedCursor(int pageSize) => Random.Shared.Next(1, LargestPageMultiple) * pageSize;

    public static Uri NewHttpsUri(string host, string? path = null) =>
        new UriBuilder(Uri.UriSchemeHttps, host) { Path = path ?? NewRequestPath() }.Uri;

    public static Uri NewHttpUri(string host) => new UriBuilder(Uri.UriSchemeHttp, host) { Path = NewRequestPath() }.Uri;

    public static Uri NewUriWithTraversal(string host) =>
        new UriBuilder(Uri.UriSchemeHttps, host) { Path = $"{NewRequestPath()}/../../{NewRequestPath()}" }.Uri;

    public static Uri NewUriWithRawSegment(string host, string rawSegment) =>
        new Uri($"{Uri.UriSchemeHttps}://{host}/{NewRequestPath()}/{rawSegment}/{NewRequestPath()}");

    public static decimal NewScoreAtOrAbove(decimal threshold) =>
        threshold + (Random.Shared.Next(0, LargestScoreStepAbove) / (decimal)TenthsPerUnit);

    public static decimal NewScoreBelow(decimal threshold) =>
        threshold - (Random.Shared.Next(1, LargestScoreStepBelow) / (decimal)TenthsPerUnit);

    public static string NewUserAgentTaggedWith(string token) => $"{NewBrowserUserAgent()} {token}/{UserAgentTagVersion}";
}
