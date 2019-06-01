namespace crgolden.Shared
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class DatabaseOptions
    {
        public string DatabaseType { get; set; }

        public bool SeedData { get; set; }

        public SqlServerOptions SqlServerOptions { get; set; }

        public SqliteOptions SqliteOptions { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnectionstringbuilder
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SqlServerOptions
    {
        public int ConnectTimeout { get; set; }

        public string DataSource { get; set; }

        public bool Encrypt { get; set; }

        public string InitialCatalog { get; set; }

        public bool IntegratedSecurity { get; set; }

        public bool MultipleActiveResultSets { get; set; }

        public string Password { get; set; }

        public bool PersistSecurityInfo { get; set; }

        public bool TrustServerCertificate { get; set; }

        public string UserId { get; set; }
    }

    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.data.sqlite.sqliteconnectionstringbuilder
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SqliteOptions
    {
        public string Cache { get; set; } = "Default";

        public string DataSource { get; set; }

        public string Mode { get; set; } = "ReadWriteCreate";
    }
}
