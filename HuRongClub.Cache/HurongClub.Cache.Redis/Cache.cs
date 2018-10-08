using Newtonsoft.Json;
using System;

namespace HuRongClub.Cache.Redis
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2016.04.28 10:45
    /// 描 述：定义缓存接口
    /// </summary>
    public class Cache : ICache
    {
        /// <summary>
        /// 读取缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        /// <returns></returns>
        public T GetCache<T>(string cacheKey) where T : class
        {
            return RedisCache.Get<T>(cacheKey);
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        public void WriteCache<T>(T value, string cacheKey) where T : class
        {
            RedisCache.Set(cacheKey, value);
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="value">对象数据</param>
        /// <param name="cacheKey">键</param>
        /// <param name="expireTime">到期时间</param>
        public void WriteCache<T>(T value, string cacheKey, DateTime expireTime) where T : class
        {
            TimeSpan span = expireTime - DateTime.Now;
            RedisCache.Set(cacheKey, JsonConvert.SerializeObject(value), span.Seconds);
        }

        /// <summary>
        /// 移除指定数据缓存
        /// </summary>
        /// <param name="cacheKey">键</param>
        public void RemoveCache(string cacheKey)
        {
            RedisCache.Remove(cacheKey);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public void RemoveCache()
        {
            RedisCache.RemoveAll();
        }
    }
}