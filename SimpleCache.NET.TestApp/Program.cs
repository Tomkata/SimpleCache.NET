using System;
using SimpleCache.NET;

namespace SimpleCache.NET.TestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var cache = new SimpleCache();

            cache.Set("greeting", "Hello, world!", TimeSpan.FromSeconds(5));
            Console.WriteLine(" Added 'greeting' to cache.");

            var value = cache.Get<string>("greeting");
            Console.WriteLine($" Retrieved value: {value}");

            Console.WriteLine($" Exists('greeting'): {cache.Exists("greeting")}");

            Console.WriteLine(" Waiting 6 seconds for cache to expire...");
            Thread.Sleep(6000);

            var expired = cache.Get<string>("greeting");
            Console.WriteLine($" After TTL: {expired ?? " (expired or not found)"}");

            cache.Set("number", 123, TimeSpan.FromMinutes(1));
            cache.Set("user", new { Name = "Alice", Age = 25 }, TimeSpan.FromMinutes(1));

            Console.WriteLine(" Added 'number' and 'user' entries.");
            Console.WriteLine($"number = {cache.Get<int>("number")}");
            var user = cache.Get<dynamic>("user");
            Console.WriteLine($"user = {user.Name}, age {user.Age}");

            Console.WriteLine($" Removed 'number', Exists: {cache.Exists("number")}");

            cache.Clear();
            Console.WriteLine(" Cleared all cache.");
            Console.WriteLine($"Exists('user'): {cache.Exists("user")}");

            var stats = cache.GetStats();
            Console.WriteLine($"Hits: {stats.Hits}, Misses: {stats.Misses}, Active: {stats.ActiveItems}");
            Console.WriteLine(" Test complete!");
        }
    }
}
