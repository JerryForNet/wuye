using HuRongClub.Util;
using StackExchange.Redis;

namespace HuRongClub.Cache.Redis
{
    public class RedisConn
    {
        #region 基础

        private static string constr = baseConfig.host + ":" + baseConfig.port + ",password=" + baseConfig.password;
        private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(constr);

        private static object m_Lock = new object();

        public static ConnectionMultiplexer Manager
        {
            get
            {
                if (redis != null)
                {
                    return redis;
                }
                else
                {
                    lock (m_Lock)
                    {
                        if (redis == null)
                        {
                            redis = ConnectionMultiplexer.Connect(constr);
                        }
                        return redis;
                    }
                }
            }
        }

        /// <summary>
        /// 获取redis的配置参数
        /// </summary>
        public static RedisSetting baseConfig
        {
            get
            {
                string configXmlPath = Utils.GetXmlMapPath("RedisConection");
                RedisSetting redisHost = (RedisSetting)XmlHelper.Load(typeof(RedisSetting), configXmlPath);
                return redisHost;
            }
        }

        #endregion
    }
}