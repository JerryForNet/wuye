using HuRongClub.Util;

namespace HuRongClub.Application.Code
{
    /// <summary>
    /// 版 本 6.1
    /// 日 期：2015.11159 10:45
    /// 描 述：系统信息
    /// </summary>
    public class SystemInfo
    {
        /// <summary>
        /// 当前Tab页面模块Id
        /// </summary>
        public static string CurrentModuleId
        {
            get
            {
                return WebHelper.GetCookie("currentmoduleId");
            }
        }

        /// <summary>
        /// 当前登录用户Id
        /// </summary>
        public static string CurrentUserId
        {
            get
            {
                return OperatorProvider.Provider.Current().UserId;
            }
        }
    }
}