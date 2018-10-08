using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HuRongClub.Cache.Redis
{

    /// <summary>
    /// Redis 配置
    /// Author:LiJun Time:2017/3/15 13:15:02
    /// Versions: 1.0.0.1
    /// </summary>
    public class RedisSetting
    {
        private bool _isopen;

        private int _cacheMinute;

        private long _db;

        private int _port;

        private string _host;

        private int _portSsl;

        private int _portSentinel;

        private string _password;

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool isopen
        {
            get { return _isopen; }
            set { _isopen = value; }
        }

        /// <summary>
        /// 缓存的时间（单位分钟）
        /// </summary>
        public int cacheMinute
        {
            get { return _cacheMinute; }
            set { _cacheMinute = value; }
        }

        /// <summary>
        /// db数据库
        /// </summary>
        public long db
        {
            get { return _db; }
            set { _db = value; }
        }

        /// <summary>
        /// 端口
        /// </summary>
        public int port
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public string host
        {
            get { return _host; }
            set { _host = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public int portSsl
        {
            get { return _portSsl; }
            set { _portSsl = value; }
        }

        /// <summary>
        ///
        /// </summary>
        public int portSentinel
        {
            get { return _portSentinel; }
            set { _portSentinel = value; }
        }

        /// <summary>
        ///  密码
        /// </summary>
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
