using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCache.NET.Models
{
    /// <summary>
    /// Represents a single value stored in the cache
    /// </summary>
    public class CacheItem
    {
       
        /// <summary>
        /// The value being cached. Can be of any type.
        /// </summary>
        public object Value { get; }
        /// <summary>
        /// The timestamp when this cache item was created.
        /// </summary>
        public DateTime CreatedAt { get; }
        /// <summary>
        /// The time-to-live duration for this item
        /// </summary>
        public TimeSpan TimeToLive { get; }

        public bool IsExpired => DateTime.UtcNow - CreatedAt > TimeToLive;

        // <summary>
        /// Initializes a new instance of the <see cref="CacheItem"/> class.
        /// </summary>
        /// <param name="value">The object to cache.</param>
        /// <param name="ttl">The time-to-live duration.</param>
        public CacheItem(object value, TimeSpan ttl)
        {
            this.Value = value;
            this.CreatedAt = DateTime.UtcNow;
            this.TimeToLive = ttl;
        }

    }
}
