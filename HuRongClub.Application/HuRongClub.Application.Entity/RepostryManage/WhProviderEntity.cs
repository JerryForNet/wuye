using System;
using HuRongClub.Application.Code;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：彭长青
    /// 日 期：2017-04-27 22:26
    /// 描 述：供应商列表
    /// </summary>
    public class WhProviderEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// fproviderid
        /// </summary>
        /// <returns></returns>
        public string fproviderid { get; set; }
        /// <summary>
        /// fname
        /// </summary>
        /// <returns></returns>
        public string fname { get; set; }
        /// <summary>
        /// ffullname
        /// </summary>
        /// <returns></returns>
        public string ffullname { get; set; }
        /// <summary>
        /// faddress
        /// </summary>
        /// <returns></returns>
        public string faddress { get; set; }
        /// <summary>
        /// fphone
        /// </summary>
        /// <returns></returns>
        public string fphone { get; set; }
        /// <summary>
        /// flinkman
        /// </summary>
        /// <returns></returns>
        public string flinkman { get; set; }
        /// <summary>
        /// ffax
        /// </summary>
        /// <returns></returns>
        public string ffax { get; set; }
        /// <summary>
        /// fpostcode
        /// </summary>
        /// <returns></returns>
        public string fpostcode { get; set; }
        /// <summary>
        /// fwebsite
        /// </summary>
        /// <returns></returns>
        public string fwebsite { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            // 主健只能为 uniqueidentifier 或 int 
                                            }
        #endregion
    }
}