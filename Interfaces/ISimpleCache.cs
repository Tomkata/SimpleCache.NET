using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCache.NET.Interfaces
{
    // <summary>
    /// Defines a basic contract for a simple in-memory cache.
    /// </summary>
    public interface ISimpleCache
    {
        /// <summary>
        /// Stores an item in the cache for a specified duration.
        /// </summary>
        /// <param name="key">Unique key for the cached item.</param>
        /// <param name="value">The object to cache.</param>
        /// <param name="ttl">Time-to-live duration for the cached item.</param>

        void Set(string key, object value, TimeSpan ttl);

        /// <summary>
        /// Retrieves an item from the cache by its key.
        /// </summary>
        /// <typeparam name="T">The expected type of the cached value.</typeparam>
        /// <param name="key">Unique key for the cached item.</param>
        /// <returns>The cached value, or default if not found or expired.</returns>
        T? Get<T>(string key);

        /// <summary>
        /// Determines whether an entry associated with the specified key exists in the collection.
        /// </summary>
        /// <param name="key">The key to locate in the collection. Cannot be null.</param>
        /// <returns>true if an entry with the specified key exists; otherwise, false.</returns>
        bool Exists(string key);

        /// <summary>
        /// Removes the element with the specified key from the collection.
        /// </summary>
        /// <param name="key">The key of the element to remove. Cannot be null.</param>
        /// <returns>true if the element is successfully removed; otherwise, false. This method also returns false if the key was
        /// not found in the collection.</returns>
        bool Remove(string key);

        /// <summary>
        /// Clears all items from the cache.
        /// </summary>
        void Clear();
    }
}
