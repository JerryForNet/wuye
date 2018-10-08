﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HuRongClub.Cache.Redis
{
    public class Redlock
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="list"></param>
        public Redlock(params ConnectionMultiplexer[] list)
        {
            foreach (var item in list)
                this.redisMasterDictionary.Add(item.GetEndPoints().First().ToString(), item);
        }

        private const int DefaultRetryCount = 3;
        private readonly TimeSpan DefaultRetryDelay = new TimeSpan(0, 0, 0, 0, 200);
        private const double ClockDriveFactor = 0.01;
        protected int Quorum { get { return (redisMasterDictionary.Count / 2) + 1; } }

        /// <summary>
        /// redis 脚本
        /// </summary>
        private const String UnlockScript = @"
            if redis.call(""get"",KEYS[1]) == ARGV[1] then
                return redis.call(""del"",KEYS[1])
            else
                return 0
            end";

        protected static byte[] CreateUniqueLockId()
        {
            return Guid.NewGuid().ToByteArray();
        }

        // 连接集合
        protected Dictionary<String, ConnectionMultiplexer> redisMasterDictionary = new Dictionary<string, ConnectionMultiplexer>();

        protected bool LockInstance(string redisServer, string resource, byte[] val, TimeSpan ttl)
        {
            bool succeeded;
            try
            {
                var redis = this.redisMasterDictionary[redisServer];
                succeeded = redis.GetDatabase().StringSet(resource, val, ttl, When.NotExists);
            }
            catch (Exception)
            {
                succeeded = false;
            }
            return succeeded;
        }

        protected void UnlockInstance(string redisServer, string resource, byte[] val)
        {
            RedisKey[] key = { resource };
            RedisValue[] values = { val };
            var redis = redisMasterDictionary[redisServer];

            redis.GetDatabase().ScriptEvaluate(UnlockScript, key, values);
        }

        public bool Lock(RedisKey resource, TimeSpan ttl, out Lock lockObject)
        {
            var val = CreateUniqueLockId();
            Lock innerLock = null;
            bool successfull = retry(DefaultRetryCount, DefaultRetryDelay, () =>
            {
                try
                {
                    int n = 0;
                    var startTime = DateTime.Now;

                    for_each_redis_registered(
                        redis =>
                        {
                            if (LockInstance(redis, resource, val, ttl)) n += 1;
                        }
                    );

                    var drift = Convert.ToInt32((ttl.TotalMilliseconds * ClockDriveFactor) + 2);
                    var validity_time = ttl - (DateTime.Now - startTime) - new TimeSpan(0, 0, 0, 0, drift);

                    if (n >= Quorum && validity_time.TotalMilliseconds > 0)
                    {
                        innerLock = new Lock(resource, val, validity_time);
                        return true;
                    }
                    else
                    {
                        for_each_redis_registered(
                            redis =>
                            {
                                UnlockInstance(redis, resource, val);
                            }
                        );
                        return false;
                    }
                }
                catch (Exception)
                { return false; }
            });

            lockObject = innerLock;
            return successfull;
        }

        protected void for_each_redis_registered(Action<ConnectionMultiplexer> action)
        {
            foreach (var item in redisMasterDictionary)
            {
                action(item.Value);
            }
        }

        protected void for_each_redis_registered(Action<String> action)
        {
            foreach (var item in redisMasterDictionary)
            {
                action(item.Key);
            }
        }

        protected bool retry(int retryCount, TimeSpan retryDelay, Func<bool> action)
        {
            int maxRetryDelay = (int)retryDelay.TotalMilliseconds;
            Random rnd = new Random();
            int currentRetry = 0;

            while (currentRetry++ < retryCount)
            {
                if (action()) return true;
                Thread.Sleep(rnd.Next(maxRetryDelay));
            }
            return false;
        }

        public void Unlock(Lock lockObject)
        {
            for_each_redis_registered(redis =>
                {
                    UnlockInstance(redis, lockObject.Resource, lockObject.Value);
                });
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.GetType().FullName);

            sb.AppendLine("Registered Connections:");
            foreach (var item in redisMasterDictionary)
            {
                sb.AppendLine(item.Value.GetEndPoints().First().ToString());
            }

            return sb.ToString();
        }
    }
}