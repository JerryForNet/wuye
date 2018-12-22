using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System;
using System.IO;
using System.Web;

namespace HuRongClub.Util.Log
{
    public class log4netHelper
    {
       
        /// <summary>
        /// 追加一条普通的日志信息
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Info(string message)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(HttpContext.Current.Server.MapPath("/XmlConfig/log4net.config")));
            log4net.ILog log = log4net.LogManager.GetLogger("InfoLog");
            if (log.IsInfoEnabled)
            {
                StringBuilder strInfo = new StringBuilder();
                strInfo.Append(System.DateTime.Now.ToString()+" \t" + message + " \r\n");
                log.Info(strInfo.ToString());
            }
            log = null;
        }

    }
}
