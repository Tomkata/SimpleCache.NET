using SimpleCache.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCache.NET.Tests
{
    public class SimpleCacheTests
    {
        [Fact]
        public void Set_And_Get_Shoud_Return_Same_Value()
        {
            var cache = new SimpleCache();
            string key = "test_key";
            string expected = "Hello Cache!";


            cache.Set(key, expected, TimeSpan.FromSeconds(5));
            var actual = cache.Get<string>(key);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Get_Should_Return_Default_When_Item_Expired()
        {
            var cache = new SimpleCache();

            string key = "short_lived";

            cache.Set(key, "temporary", TimeSpan.FromMilliseconds(500));
            Thread.Sleep(1000);
            var value = cache.Get<string>(key);

            Assert.Null(value);
        }

        [Fact]
        public void Exists_Should_Return_True_For_Valid_Item()
        {
            var cache = new SimpleCache();

            cache.Set("active", 42, TimeSpan.FromMinutes(1));
            bool isExists = cache.Exists("active");

            Assert.True(isExists);
        }

        [Fact]
        public void Remove_Should_Delete_Item()
        {
            var cache = new SimpleCache();
            cache.Set("delete_me", "data", TimeSpan.FromMinutes(1));

            bool removed = cache.Remove("delete_me");
            bool stillExists = cache.Exists("delete_me");


            Assert.True(removed);
            Assert.False(stillExists);
        }

        [Fact]
        public void Clear_Should_Remove_All_Items()
        {
            var cache = new SimpleCache();
            cache.Set("a", 1, TimeSpan.FromMinutes(1));
            cache.Set("b", 2, TimeSpan.FromMinutes(1));

            cache.Clear();

            Assert.False(cache.Exists("a"));
            Assert.False(cache.Exists("b"));
        }
    }
}
