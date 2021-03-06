﻿using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace HuRongClub.Cache.Redis
{
    /// <summary>
    /// Redis 辅助类
    /// </summary>
    public partial class RedisCache
    {
        private static ConnectionMultiplexer RedisConnectionManager = RedisConn.Manager;

        #region set redis

        public static void Set(string key, string value, int expiretime = 0)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    var Redis = RedisConnectionManager.GetDatabase();
                    if (expiretime != 0)
                    {
                        TimeSpan span = DateTime.Now.AddSeconds(expiretime) - DateTime.Now;
                        Redis.StringSet(key, value, span);
                    }
                    else
                    {
                        Redis.StringSet(key, value);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void Set<T>(string key, T value, int expiretime = 0)
        {
            if (value != null)
            {
                try
                {
                    var Redis = RedisConnectionManager.GetDatabase();
                    if (expiretime != 0)
                    {
                        TimeSpan span = DateTime.Now.AddSeconds(expiretime) - DateTime.Now;
                        Redis.StringSet(key, JsonConvert.SerializeObject(value), span);
                    }
                    else
                    {
                        Redis.StringSet(key, JsonConvert.SerializeObject(value));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void SetHash(string key, Dictionary<string, string> dict, int expiretime = 0)
        {
            if (dict != null)
            {
                HashEntry[] hashEntries = new HashEntry[dict.Count];
                int i = 0;
                foreach (var item in dict)
                {
                    hashEntries[i] = new HashEntry(item.Key, item.Value);
                    i++;
                }
                var Redis = RedisConnectionManager.GetDatabase();
                Redis.HashSet(key, hashEntries);
                if (expiretime != 0)
                {
                    TimeSpan span = DateTime.Now.AddSeconds(expiretime) - DateTime.Now;
                    Redis.KeyExpire(key, span);
                }
            }
        }

        /// <summary>
        /// 存在并发问题，慎用
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="list"></param>
        /// <param name="expiretime"></param>
        public static void SetList<T>(string key, IList<T> list, int expiretime = 0)
        {
            if (list != null && list.Count > 0)
            {
                RedisValue[] lists = new RedisValue[list.Count];

                try
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        lists[i] = JsonConvert.SerializeObject(list[i]);
                    }

                    bool result = DoLock(key, () =>
                    {
                        var Redis = RedisConnectionManager.GetDatabase();
                        Redis.KeyDelete(key);
                        long r = Redis.ListRightPush(key, lists);
                        if (expiretime != 0)
                        {
                            TimeSpan span = DateTime.Now.AddSeconds(expiretime) - DateTime.Now;
                            Redis.KeyExpire(key, span);
                        }

                        return true;
                    });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 在集合中插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public static void SetListItem<T>(string key, int index, T value)
        {
            if (value != null)
            {
                try
                {
                    var Redis = RedisConnectionManager.GetDatabase();
                    Redis.ListSetByIndex(key, index, JsonConvert.SerializeObject(value));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void SetListItem(string key, int index, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    var Redis = RedisConnectionManager.GetDatabase();
                    Redis.ListSetByIndex(key, index, value);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void AppendList<T>(string key, T value)
        {
            if (value != null)
            {
                try
                {
                    var Redis = RedisConnectionManager.GetDatabase();
                    Redis.ListRightPush(key, JsonConvert.SerializeObject(value));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static void AppendList<T>(string key, List<T> value)
        {
            if (value != null)
            {
                RedisValue[] lists = new RedisValue[value.Count];
                try
                {
                    if (value != null && value.Count > 0)
                    {
                        for (int i = 0; i < value.Count; i++)
                        {
                            lists[i] = JsonConvert.SerializeObject(value[i]);
                        }
                        var Redis = RedisConnectionManager.GetDatabase();
                        Redis.ListRightPush(key, lists);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region get redis

        public static string Get(string key)
        {
            try
            {
                var Redis = RedisConnectionManager.GetDatabase();
                return Redis.StringGet(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T Get<T>(string key) where T : class
        {
            try
            {
                var Redis = RedisConnectionManager.GetDatabase();
                string sg = Redis.StringGet(key);
                if (!string.IsNullOrEmpty(sg))
                {
                    return JsonConvert.DeserializeObject<T>(sg);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Dictionary<string, string> GetHash(string key)
        {
            try
            {
                Dictionary<string, string> dict = new Dictionary<string, string>();
                var Redis = RedisConnectionManager.GetDatabase();
                HashEntry[] hashentries = Redis.HashGetAll(key);
                if (hashentries != null && hashentries.Length > 0)
                {
                    foreach (var item in hashentries)
                    {
                        dict.Add(item.Name, item.Value);
                    }
                    return dict;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<T> GetList<T>(string key) where T : class
        {
            try
            {
                List<T> listitem = new List<T>();
                List<string> list = new List<string>();
                var Redis = RedisConnectionManager.GetDatabase();
                long count = Redis.ListLength(key);
                for (int i = 0; i < count; i += 1000)
                {
                    list.AddRange(GetList(key, i, i + 999));
                }

                if (list != null && list.Count > 0)
                {
                    foreach (string item in list)
                    {
                        listitem.Add(JsonConvert.DeserializeObject<T>(item));
                    }
                }
                if (listitem.Count > 0)
                {
                    return listitem;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<string> GetList(string key, int start, int end)
        {
            var Redis = RedisConnectionManager.GetDatabase();
            long count = Redis.ListLength(key);
            if (count < end)
            {
                end = (int)count;
            }
            RedisValue[] list = Redis.ListRange(key, start, end);

            List<string> listitem = null; ;
            if (list != null && list.Length > 0)
            {
                listitem = new List<string>();
                foreach (string item in list)
                {
                    listitem.Add(item);
                }
            }
            return listitem;
        }

        public static List<string> GetList(string key)
        {
            try
            {
                var Redis = RedisConnectionManager.GetDatabase();
                long count = Redis.ListLength(key);

                List<string> returnlist = new List<string>();

                for (int i = 0; i < count; i += 1000)
                {
                    returnlist.AddRange(GetList(key, i, i + 999));
                }
                if (returnlist.Count > 0)
                {
                    return returnlist;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T GetListItem<T>(string key, int index) where T : class
        {
            try
            {
                var Redis = RedisConnectionManager.GetDatabase();
                var item = Redis.ListGetByIndex(key, index);
                return JsonConvert.DeserializeObject<T>(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GetListItem(string key, int index)
        {
            try
            {
                var Redis = RedisConnectionManager.GetDatabase();
                return Redis.ListGetByIndex(key, index);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region remove

        public static bool Remove(string key)
        {
            try
            {
                var Redis = RedisConnectionManager.GetDatabase();
                return Redis.KeyDelete(key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int RemoveAll(List<string> keys)
        {
            int result = 0;
            try
            {
                foreach (string item in keys)
                {
                    if (Remove(item)) result++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// redis 中 根据 value 删除 List 中的某一项
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static bool ListRemove(string key, string value)
        {
            try
            {
                var Redis = RedisConnectionManager.GetDatabase();
                if (Redis.KeyExists(key))
                {
                    long r = Redis.ListRemove(key, value);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return false;
        }

        /// <summary>
        /// 删除所有key
        /// </summary>
        public static void RemoveAll()
        {
            try
            {
                var Redis = RedisConnectionManager.GetDatabase();
                Redis.ScriptEvaluate(LuaScript.Prepare(
                  //Redis的keys模糊查询：
                  " local ks = redis.call('KEYS', @keypattern) " + //local ks为定义一个局部变量，其中用于存储获取到的keys
                  " for i=1,#ks,5000 do " +    //#ks为ks集合的个数, 语句的意思： for(int i = 1; i <= ks.Count; i+=5000)
                  "     redis.call('del', unpack(ks, i, math.min(i+4999, #ks))) " + //Lua集合索引值从1为起始，unpack为解包，获取ks集合中的数据，每次5000，然后执行删除
                  " end " +
                  " return true "
                  ),
                 new { keypattern = "*" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region tool

        public static bool KeyExpire(string key, int expiretime)
        {
            try
            {
                TimeSpan span = DateTime.Now.AddSeconds(expiretime) - DateTime.Now;
                var Redis = RedisConnectionManager.GetDatabase();
                return Redis.KeyExpire(key, span);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 分布式锁操作
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="value"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected static bool DoLock(string redisKey, Func<bool> action)
        {
            if (string.IsNullOrEmpty(redisKey)) return false;
            bool result = false;
            string resource = redisKey + "_lock";

            try
            {
                var dlm = new Redlock(RedisConnectionManager);

                Lock lockObject;

                var locked = dlm.Lock(
                    resource,
                    new TimeSpan(0, 0, 10),
                    out lockObject);

                result = action();
                dlm.Unlock(lockObject);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        #endregion
    }
}