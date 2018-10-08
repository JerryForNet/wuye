using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.RepostryManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-01 15:26
    /// 描 述：供应商管理
    /// </summary>
    public class ProviderEntity : BaseEntity
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

        #endregion 实体成员

        #region 扩展操作

        //public override void Create()
        //{
        //    this.fproviderid = Guid.NewGuid().ToString();
        //}

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.fproviderid = keyValue;
        }

        #endregion 扩展操作
    }
}