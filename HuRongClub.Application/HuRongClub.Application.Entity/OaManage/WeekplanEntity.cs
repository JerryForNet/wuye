using HuRongClub.Application.Code;
using System;

namespace HuRongClub.Application.Entity.OaManage
{
    /// <summary>
    /// 版 本：HurongClub.Framework V1.0.1
    /// 创 建：王菲
    /// 日 期：2017-04-26 16:05
    /// 描 述：工作周记
    /// </summary>
    public class WeekplanEntity : BaseEntity
    {
        #region 实体成员

        /// <summary>
        /// id
        /// </summary>
        /// <returns></returns>
        public string id { get; set; }

        /// <summary>
        /// title
        /// </summary>
        /// <returns></returns>
        public string title { get; set; }

        /// <summary>
        /// planning
        /// </summary>
        /// <returns></returns>
        public string planning { get; set; }

        /// <summary>
        /// summary
        /// </summary>
        /// <returns></returns>
        public string summary { get; set; }

        /// <summary>
        /// notes
        /// </summary>
        /// <returns></returns>
        public string notes { get; set; }

        /// <summary>
        /// userid
        /// </summary>
        /// <returns></returns>
        public string userid { get; set; }

        /// <summary>
        /// inputtime
        /// </summary>
        /// <returns></returns>
        public DateTime? inputtime { get; set; }

        /// <summary>
        /// ifcheck
        /// </summary>
        /// <returns></returns>
        public string ifcheck { get; set; }

        /// <summary>
        /// checktime
        /// </summary>
        /// <returns></returns>
        public DateTime? checktime { get; set; }

        #endregion 实体成员

        #region 扩展操作

        /// <summary>
        /// 新增调用
        /// </summary>
        //public override void Create()
        //{
        //}

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.id = keyValue;
        }

        #endregion 扩展操作
    }
}