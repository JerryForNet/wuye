using System.Configuration;

namespace HuRongClub.Cache.Redis
{
    public sealed class RedisConfigInfo : ConfigurationSection
    {
        public static RedisConfigInfo GetConfig()
        {
            RedisConfigInfo section = (RedisConfigInfo)ConfigurationManager.GetSection("redisconfig");
            if (section == null)
                throw new ConfigurationErrorsException("Section redisconfig is not found.");
            return section;
        }

        public static RedisConfigInfo GetConfig(string sectionName)
        {
            RedisConfigInfo section = (RedisConfigInfo)ConfigurationManager.GetSection("redisconfig");
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }

        /// <summary>
        /// 可写的Redis链接地址
        /// </summary>
        [ConfigurationProperty("Server", IsRequired = false)]
        public string Server
        {
            get
            {
                return (string)base["Server"];
            }
            set
            {
                base["Server"] = value;
            }
        }

        /// <summary>
        /// 最大写链接数
        /// </summary>
        [ConfigurationProperty("MaxWritePoolSize", IsRequired = false, DefaultValue = 5)]
        public int MaxWritePoolSize
        {
            get
            {
                int _maxWritePoolSize = (int)base["MaxWritePoolSize"];
                return _maxWritePoolSize > 0 ? _maxWritePoolSize : 5;
            }
            set
            {
                base["MaxWritePoolSize"] = value;
            }
        }

        /// <summary>
        /// 最大读链接数
        /// </summary>
        [ConfigurationProperty("MaxReadPoolSize", IsRequired = false, DefaultValue = 5)]
        public int MaxReadPoolSize
        {
            get
            {
                int _maxReadPoolSize = (int)base["MaxReadPoolSize"];
                return _maxReadPoolSize > 0 ? _maxReadPoolSize : 5;
            }
            set
            {
                base["MaxReadPoolSize"] = value;
            }
        }

        /// <summary>
        /// 自动重启
        /// </summary>
        [ConfigurationProperty("AutoStart", IsRequired = false, DefaultValue = true)]
        public bool AutoStart
        {
            get
            {
                return (bool)base["AutoStart"];
            }
            set
            {
                base["AutoStart"] = value;
            }
        }

        /// <summary>
        /// 默认缓存到期时间，单位:秒
        /// </summary>
        [ConfigurationProperty("LocalCacheTime", IsRequired = false, DefaultValue = 36000)]
        public int LocalCacheTime
        {
            get
            {
                return (int)base["LocalCacheTime"];
            }
            set
            {
                base["LocalCacheTime"] = value;
            }
        }

        /// <summary>
        /// 是否记录日志,该设置仅用于排查redis运行时出现的问题,如redis工作正常,请关闭该项
        /// </summary>
        [ConfigurationProperty("RecordeLog", IsRequired = false, DefaultValue = false)]
        public bool RecordeLog
        {
            get
            {
                return (bool)base["RecordeLog"];
            }
            set
            {
                base["RecordeLog"] = value;
            }
        }


        /// <summary>
        /// 密码
        /// </summary>
        [ConfigurationProperty("Password", IsRequired = false, DefaultValue = false)]
        public string Password
        {
            get
            {
                return (string)base["Password"];
            }
            set
            {
                base["Password"] = value;
            }
        }
    }
}