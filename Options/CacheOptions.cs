namespace Clarity.Shared
{
    using System;

    public class CacheOptions
    {
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

        public TimeSpan? SlidingExpiration { get; set; }

        public long? Size { get; set; }
    }
}
