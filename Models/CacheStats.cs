using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCache.NET.Models
{
    /// <summary>
    /// Represents basics statistics about the cache usage.
    /// </summary>
  
    public class CacheStats
    {
        /// <summary>
        /// Number of successful cache hits (found items).
        ///</summary>
        public int Hits { get; internal set; }

        /// <summary>
        /// Number of cache misses (not found or expired items).
        /// </summary>
        public int Misses { get;internal set; }

        /// <summary>
        /// Number of currently active (valid) cache items.
        /// </summary>
        public int ActiveItems { get; internal set; }

        /// <summary>
        /// The timestamp when the stats were last updated.
        /// </summary>
        public DateTime LastUpdated { get; internal set; }
    }
}
