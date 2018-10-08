namespace HuRongClub.Cache.Factory
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2015.11.9 10:45
    /// 描 述：缓存工厂类
    /// </summary>
    public class CacheFactory
    {
        public static CacheKey CacheKey()
        {
            return new CacheKey();
        }

        /// <summary>
        /// 定义通用的Repository
        /// </summary>
        /// <returns></returns>
        public static ICache Cache()
        {
            return new Cache();
        }

        /// <summary>
        /// 根据dbname获取redis实例
        /// </summary>
        /// <param name="dbname"></param>
        /// <returns></returns>
        public static Redis.RedisCacheNew RedisCache(int dbname)
        {
            return new Redis.RedisCacheNew(Redis.RedisConn.Manager, dbname);
        }
    }
}