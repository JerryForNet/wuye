using HuRongClub.Cache.Redis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HuRongClub.Test.Cache
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RedisSetGet()
        {
            RedisCache.Set("testkey1", "testvalue333333");

            string val = RedisCache.Get("testkey1");
        }
    }
}