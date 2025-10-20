using SimpleCache.NET.Interfaces;
using SimpleCache.NET.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCache.NET
{
    /// <summary>
    /// A simple in-memory cache implementation.
    /// </summary>
    public class SimpleCache : ISimpleCache
    {
        // <summary>
        /// Internal thread-safe dictionary that stores cache items.
        /// </summary>
        private readonly ConcurrentDictionary<string, CacheItem> _cache = new();

        private readonly CacheStats _stats = new();
        
        /// Adds or updates an item in the cache with a specified time-to-live (TTL).
        /// </summary>
        /// <param name="key">Unique key for the cached item.</param>
        /// <param name="value">The object to cache.</param>
        /// <param name="ttl">Time-to-live duration for the cached item.</param>
        public void Set(string key, object value, TimeSpan ttl)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or empty.",nameof(key));

            if(ttl<TimeSpan.Zero)
                throw new ArgumentException("TTL must be greater than zero.", nameof(ttl));

            var item = new CacheItem(value,ttl);
            _cache[key] = item;

        }

        /// <summary>
        /// Retrieves an item from the cache by its key.
        /// Returns default(T) if the item does not exist or has expired.
        /// </summary>
        /// <typeparam name="T">The expected type of the cached object.</typeparam>
        /// <param name="key">Unique key for the cached item.</param>
        /// <returns>The cached value, or default if not found or expired.</returns>
        public T? Get<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or empty", nameof(key));

            if(_cache.TryGetValue(key,out CacheItem? item))
            {
                if (item.IsExpired)
                {
                    _cache.TryRemove(key,out _);
                    _stats.Misses++;
                    _stats.LastUpdated = DateTime.UtcNow;
                    return default;
                }

                _stats.Hits++;
                _stats.LastUpdated = DateTime.UtcNow;
                return (T)item.Value;
            }

            _stats.Misses++;
            _stats.LastUpdated = DateTime.UtcNow;
            return default;
        }

        /// <summary>
        /// Removes all items from the cache.
        /// </summary>
        public void Clear()
        {
            if (_cache.IsEmpty)
                return; 

            _cache.Clear();
        }

        /// <summary>
        /// Checks whether a cache entry exists and is still valid (not expired).
        /// </summary>
        /// <param name="key">Unique key for the cached item.</param>
        /// <returns>True if the key exists and the item is valid; otherwise false.</returns

        public bool Exists(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or empty.", nameof(key));

            if (_cache.TryGetValue(key, out CacheItem? item))
            {
                if (item.IsExpired)
                {
                    _cache.TryRemove(key, out _);
                    return false;
                }

                return true; 
            }

            return false; 
        }


        /// <summary>
        /// Removes a specific item from the cache.
        /// </summary>
        /// <param name="key">Unique key for the cached item.</param>
        public bool Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Cache key cannot be null or empty.", nameof(key));

            if (_cache.TryRemove(key, out CacheItem? item))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a snapshot of current cache statistics.
        /// </summary>
        public CacheStats GetStats()
        {
            _stats.ActiveItems = _cache.Count;
            _stats.LastUpdated = DateTime.UtcNow;
            return _stats;
        }

    }
}
